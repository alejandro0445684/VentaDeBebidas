using UnityEngine;
using UnityEngine.UI; // Necesario para la UI

public class ProductoEnMesa : MonoBehaviour
{
    [Header("Datos del Producto")]
    [SerializeField] private ProductoData datos;

    [Header("Referencias Visuales")]
    [SerializeField] private Image imagenProducto; // Cambiado a Image

    public ProductoData Datos => datos;

    public void InicializarProducto(ProductoData nuevosDatos)
    {
        datos = nuevosDatos;

        if (imagenProducto != null && datos != null)
        {
            imagenProducto.sprite = datos.spriteProducto;
        }
    }
}
