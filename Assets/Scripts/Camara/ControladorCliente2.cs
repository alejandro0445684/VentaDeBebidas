using UnityEngine;

public class ControladorCliente2 : MonoBehaviour
{
    [Header("Cámaras")]
    [SerializeField] private GameObject camaraAnterior; // La cámara del cliente 1 o general que quieres apagar
    [SerializeField] private GameObject camaraCliente2;  // La cámara exclusiva del cliente 2

    [Header("Canvas de UI")]
    [SerializeField] private GameObject canvasAnterior;  // El canvas anterior que quieres apagar
    [SerializeField] private GameObject canvasCliente2;  // El canvas exclusivo del cliente 2

    // Función para activar todo lo del cliente 2
    public void ActivarCliente2()
    {
        // 1. Apagamos lo anterior
        if (camaraAnterior != null) camaraAnterior.SetActive(false);
        if (canvasAnterior != null) canvasAnterior.SetActive(false);

        // 2. Encendemos lo del cliente 2
        if (camaraCliente2 != null) camaraCliente2.SetActive(true);
        if (canvasCliente2 != null) canvasCliente2.SetActive(true);
    }
}