using UnityEngine;

public class ControladorCamaras : MonoBehaviour
{
    [Header("Cámaras del Juego")]
    [SerializeField] private GameObject camaraPrincipal; // La cámara inicial (visión general)
    [SerializeField] private GameObject camaraCajero;    // La cámara del mostrador

    [Header("Interfaz del Cajero")]
    [SerializeField] private GameObject panelVistaCajero;
    [SerializeField] private Canvas canvasJuego;

    // Esta función se activará al presionar el botón
    public void ActivarVistaCajero()
    {
        if (camaraPrincipal != null && camaraCajero != null)
        {
            camaraPrincipal.SetActive(false); // Apaga la cámara lejana
            camaraCajero.SetActive(true);    // Enciende la cámara del cajero
        }
        if (canvasJuego != null && camaraCajero != null)
        {
            canvasJuego.worldCamera = camaraCajero.GetComponent<Camera>();
        }
        if (panelVistaCajero != null)
        {
            panelVistaCajero.SetActive(true);
        }
    }
}