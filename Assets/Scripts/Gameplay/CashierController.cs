using System.Collections.Generic;
using UnityEngine;
using VentaDeBebidas.Data;

namespace VentaDeBebidas.Gameplay
{
    /// <summary>
    /// Controla el flujo del rol Cajero: atiende clientes en orden, calcula
    /// el total del pedido, recibe el pago y valida el vuelto que el
    /// jugador entrega. Se conecta con ScoreSystem para registrar
    /// aciertos/errores.
    /// </summary>
    public class CashierController : MonoBehaviour
    {
        [Header("Referencias")]
        [SerializeField] private Customer customerPrefab; // TODO: asignar en el Inspector
        [SerializeField] private Transform customerSpawnPoint; // TODO: asignar en el Inspector
        [SerializeField] private ScoreSystem scoreSystem; // TODO: asignar en el Inspector

        private LevelData _level;
        private Queue<CustomerEntry> _pendingCustomers = new Queue<CustomerEntry>();
        private Customer _currentCustomer;
        private int _cashRegister; // dinero actual en caja

        public int CashRegister => _cashRegister;
        public Customer CurrentCustomer => _currentCustomer;

        public System.Action<int> OnCashRegisterChanged;
        public System.Action<Customer> OnNewCustomer;
        public System.Action OnLevelFinished;

        public void StartLevel(LevelData level)
        {
            _level = level;
            _cashRegister = level.startingMoney;
            scoreSystem.Init(level.customers.Count);

            _pendingCustomers = new Queue<CustomerEntry>(level.customers);
            NextCustomer();
        }

        private void NextCustomer()
        {
            if (_pendingCustomers.Count == 0)
            {
                OnLevelFinished?.Invoke();
                return;
            }

            var data = _pendingCustomers.Dequeue();
            _currentCustomer = Instantiate(customerPrefab, customerSpawnPoint.position, Quaternion.identity);
            _currentCustomer.Setup(data);
            _currentCustomer.OnPatienceExpired += HandleCustomerLeftAngry;
            OnNewCustomer?.Invoke(_currentCustomer);
        }

        private void HandleCustomerLeftAngry(Customer customer)
        {
            scoreSystem.RegisterError();
            Destroy(customer.gameObject);
            _currentCustomer = null;
            NextCustomer();
        }

        /// <summary>
        /// El jugador confirma el cobro: recibe el pago del cliente y
        /// entrega el vuelto que decidió. Se valida contra el total real.
        /// </summary>
        /// <param name="changeGiven">Vuelto que el jugador decidió entregar.</param>
        public bool ConfirmSale(int changeGiven)
        {
            if (_currentCustomer == null) return false;

            int total = _currentCustomer.CalculateOrderTotal();
            int payment = _currentCustomer.Data.paymentAmount;
            int correctChange = Mathf.Max(0, payment - total);

            bool correct = changeGiven == correctChange;

            if (correct)
            {
                _cashRegister += payment - changeGiven;
                scoreSystem.RegisterSuccess();
            }
            else
            {
                // El error de vuelto igual mueve la caja con lo que el jugador entregó,
                // para que se note el desbalance al final del turno.
                _cashRegister += payment - changeGiven;
                scoreSystem.RegisterError();
            }

            OnCashRegisterChanged?.Invoke(_cashRegister);

            _currentCustomer.MarkServed();
            Destroy(_currentCustomer.gameObject);
            _currentCustomer = null;

            NextCustomer();
            return correct;
        }
    }
}
