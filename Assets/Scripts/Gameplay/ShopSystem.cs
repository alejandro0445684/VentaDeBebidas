using System.Collections.Generic;
using UnityEngine;
using VentaDeBebidas.Data;

namespace VentaDeBebidas.Gameplay
{
    /// <summary>
    /// Administra el catálogo de productos disponibles en el nivel actual
    /// (la "góndola"). La UI de compra/venta consulta esta clase para
    /// saber qué mostrar y a qué precio.
    /// </summary>
    public class ShopSystem : MonoBehaviour
    {
        private List<Product> _availableProducts = new List<Product>();

        public IReadOnlyList<Product> AvailableProducts => _availableProducts;

        public void Init(LevelData level)
        {
            _availableProducts = new List<Product>(level.availableProducts);
        }

        public Product GetProduct(string productName)
        {
            return _availableProducts.Find(p => p.productName == productName);
        }

        public int GetPrice(Product product)
        {
            if (product == null) return 0;
            return product.isCombo && product.comboQuantity > 0
                ? product.price // el precio del combo ya es el precio total del combo
                : product.price;
        }
    }
}
