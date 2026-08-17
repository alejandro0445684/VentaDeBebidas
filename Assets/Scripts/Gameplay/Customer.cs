using UnityEngine;
using VentaDeBebidas.Data;

namespace VentaDeBebidas.Gameplay
{
    /// <summary>
    /// Controla a un cliente NPC en el rol Cajero: muestra su pedido,
    /// cuenta la paciencia (si el nivel la define) y dispara eventos
    /// cuando se va satisfecho o se cansa de esperar.
    /// Colgar este script del prefab "Customer".
    /// </summary>
    public class Customer : MonoBehaviour
    {
        public CustomerEntry Data { get; private set; }
        public bool HasLeft { get; private set; }

        private float _remainingPatience;
        private bool _hasPatienceLimit;

        [Header("Referencias visuales")]
        [SerializeField] private SpriteRenderer spriteRenderer; // TODO: asignar en el Inspector
        // TODO: asignar en el Inspector un ícono/burbuja de pedido si se usa UI sobre el personaje

        public System.Action<Customer> OnPatienceExpired;

        public void Setup(CustomerEntry data)
        {
            Data = data;
            HasLeft = false;
            _hasPatienceLimit = data.patienceSeconds > 0f;
            _remainingPatience = data.patienceSeconds;
        }

        private void Update()
        {
            if (HasLeft || !_hasPatienceLimit) return;

            _remainingPatience -= Time.deltaTime;
            if (_remainingPatience <= 0f)
            {
                HasLeft = true;
                OnPatienceExpired?.Invoke(this);
            }
        }

        public float PatienceRatio =>
            _hasPatienceLimit ? Mathf.Clamp01(_remainingPatience / Data.patienceSeconds) : 1f;

        public void MarkServed()
        {
            HasLeft = true;
        }

        /// <summary>Total que debería costar el pedido de este cliente según el catálogo actual.</summary>
        public int CalculateOrderTotal()
        {
            int total = 0;
            foreach (var entry in Data.order)
            {
                if (entry.product != null)
                    total += entry.product.price * entry.quantity;
            }
            return total;
        }
    }
}
