using System.Collections.Generic;
using UnityEngine;
using VentaDeBebidas.Data;

namespace VentaDeBebidas.Gameplay
{
    /// <summary>
    /// Controla el flujo del rol Comprador: el jugador arranca con un
    /// presupuesto (LevelData.startingMoney), debe agregar al carrito los
    /// productos de la lista de compra sin excederse, y confirmar la compra.
    /// </summary>
    public class BuyerController : MonoBehaviour
    {
        [Header("Referencias")]
        [SerializeField] private ScoreSystem scoreSystem; // TODO: asignar en el Inspector

        private LevelData _level;
        private int _budget;
        private int _spent;
        private readonly Dictionary<Product, int> _cart = new Dictionary<Product, int>();

        public int RemainingBudget => _budget - _spent;
        public int Budget => _budget;
        public IReadOnlyDictionary<Product, int> Cart => _cart;

        public System.Action<int> OnRemainingBudgetChanged;
        public System.Action OnPurchaseInvalid; // se excedió del presupuesto
        public System.Action OnLevelFinished;

        public void StartLevel(LevelData level)
        {
            _level = level;
            _budget = level.startingMoney;
            _spent = 0;
            _cart.Clear();
            scoreSystem.Init(CountRequiredItems(level));
        }

        private int CountRequiredItems(LevelData level)
        {
            int count = 0;
            foreach (var entry in level.shoppingList) count += entry.quantity;
            return Mathf.Max(1, count);
        }

        /// <summary>Intenta agregar una unidad del producto al carrito. Devuelve false si no alcanza el presupuesto.</summary>
        public bool AddToCart(Product product)
        {
            int price = product.price;
            if (_spent + price > _budget)
            {
                OnPurchaseInvalid?.Invoke();
                return false;
            }

            _spent += price;
            _cart[product] = _cart.TryGetValue(product, out var qty) ? qty + 1 : 1;
            OnRemainingBudgetChanged?.Invoke(RemainingBudget);
            return true;
        }

        public void RemoveFromCart(Product product)
        {
            if (!_cart.ContainsKey(product) || _cart[product] <= 0) return;

            _cart[product]--;
            _spent -= product.price;
            if (_cart[product] <= 0) _cart.Remove(product);
            OnRemainingBudgetChanged?.Invoke(RemainingBudget);
        }

        /// <summary>El jugador confirma la compra final. Valida contra la lista requerida del nivel.</summary>
        public bool ConfirmPurchase()
        {
            bool listComplete = true;

            foreach (var required in _level.shoppingList)
            {
                int haveQty = _cart.TryGetValue(required.product, out var qty) ? qty : 0;
                if (haveQty < required.quantity)
                {
                    listComplete = false;
                }
            }

            if (listComplete)
            {
                scoreSystem.RegisterSuccess();
            }
            else
            {
                scoreSystem.RegisterError();
            }

            OnLevelFinished?.Invoke();
            return listComplete;
        }
    }
}
