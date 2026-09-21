using UnityEngine;

[CreateAssetMenu(fileName = "NuevoProducto", menuName = "Juego/Producto Data")]
public class ProductoData : ScriptableObject
{
    [Header("Información Básica")]
    public string nombreProducto = "Producto";
    public float precio = 10.0f;

    [Header("Visuales")]
    public Sprite spriteProducto; // Para UI o Sprite 2D en la cinta/mesa
}