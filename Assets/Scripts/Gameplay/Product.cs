using UnityEngine;

namespace VentaDeBebidas.Gameplay
{
    /// <summary>
    /// Representa una bebida vendible en la tienda.
    /// Crear assets: Assets > Create > VentaDeBebidas > Product
    /// </summary>
    [CreateAssetMenu(fileName = "Product_", menuName = "VentaDeBebidas/Product")]
    public class Product : ScriptableObject
    {
        [Header("Datos básicos")]
        public string productName = "Gaseosa";
        public Sprite icon; // TODO: asignar sprite en el Inspector
        [Min(0)] public int price = 3000; // en guaraníes (o la moneda que uses)

        [Header("Opcional")]
        [TextArea] public string description;
        public bool isCombo = false;       // ej. "2x1"
        public int comboQuantity = 1;      // cuántas unidades incluye el combo
    }
}
