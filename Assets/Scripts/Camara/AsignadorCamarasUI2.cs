using UnityEngine;

public class ControladorVistasCliente2 : MonoBehaviour
{
    [Header("Cámaras")]
    public Camera camaraCliente2;
    public Camera camaraCajero;

    [Header("Canvas de UI")]
    public Canvas canvasUICliente2;      // El canvas que flota con el cliente 2
    public GameObject canvasCajero2;     // El GameObject del canvas/panel que debe prenderse en el cajero

    // Llama a esta función cuando el jugador va a la vista del Cajero desde el Cliente 2
    public void IrAVistaCajero2()
    {
        // 1. Apagar la cámara del cliente 2 y activar la del cajero
        if (camaraCliente2 != null) camaraCliente2.gameObject.SetActive(false);
        if (camaraCajero != null) camaraCajero.gameObject.SetActive(true);

        // 2. Apagar el Canvas del cliente 2 y encender el Canvas del cajero
        if (canvasUICliente2 != null) canvasUICliente2.gameObject.SetActive(false);
        if (canvasCajero2 != null) canvasCajero2.SetActive(true);

        // 3. Reasignar la Event Camera del Canvas al Cajero para que detecte clics (si el canvas del cajero usa world space)
        // Nota: Si tu canvas de cajero es Screen Space - Overlay, no necesita event camera.
    }

    // Llama a esta función para volver a la vista del Cliente 2
    public void VolverAVistaCliente2()
    {
        if (camaraCajero != null) camaraCajero.gameObject.SetActive(false);
        if (camaraCliente2 != null) camaraCliente2.gameObject.SetActive(true);

        if (canvasCajero2 != null) canvasCajero2.SetActive(false);
        if (canvasUICliente2 != null) canvasUICliente2.gameObject.SetActive(true);
    }
}