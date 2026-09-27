using UnityEngine;

public class ControladorCamaras : MonoBehaviour
{
    [Header("Cámaras del Juego")]
    [SerializeField] private GameObject camaraPrincipal; // La cámara inicial (visión general)
    [SerializeField] private GameObject camaraCajero;    // La cámara del mostrador
    [SerializeField] private GameObject camara2doCliente; // Camara del segundo cliente

    [Header("Canvas de UI")]
    [SerializeField] private GameObject canvasJuego;     // El Canvas Principal
    [SerializeField] private GameObject canvasCajero;    // El nuevo Canvas del Cajero

    [Header("Elementos del Cajero")]
    [SerializeField] private GameObject panelVistaCajero;

    // Esta función se activará al presionar el botón para IR AL CAJERO
    public void ActivarVistaCajero()
    {
        // 1. Alternar Cámaras
        if (camaraPrincipal != null && camaraCajero != null)
        {
            camaraPrincipal.SetActive(false);
            camaraCajero.SetActive(true);
        }

        // 2. Encender Canvas del Cajero y Apagar Canvas General (evita pantallas blancas o parpadeos)
        if (canvasJuego != null) canvasJuego.SetActive(false);
        if (canvasCajero != null) canvasCajero.SetActive(true);

        // 3. Activar el Panel del Cajero
        if (panelVistaCajero != null)
        {
            panelVistaCajero.SetActive(true);
        }
    }

    // Esta función sirva para VOLVER a la vista general (por si tienes un botón de regresar)
    public void VolverAVistaGeneral()
    {
        if (camaraPrincipal != null && camaraCajero != null)
        {
            camaraPrincipal.SetActive(true);
            camaraCajero.SetActive(false);
        }

        if (canvasJuego != null) canvasJuego.SetActive(true);
        if (canvasCajero != null) canvasCajero.SetActive(false);
    }
}
