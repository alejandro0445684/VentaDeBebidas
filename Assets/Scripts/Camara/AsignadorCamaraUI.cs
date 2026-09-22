using UnityEngine;

public class ControladorVistas : MonoBehaviour
{
    [Header("Cámaras")]
    public Camera mainCamera;
    public Camera camaraCajero;

    [Header("Canvas de UI")]
    public Canvas canvasUI;

    // Llama a esta función cuando el jugador va a la Vista del Cajero
    public void IrAVistaCajero()
    {
        if (mainCamera != null) mainCamera.gameObject.SetActive(false);
        if (camaraCajero != null) camaraCajero.gameObject.SetActive(true);
        if (canvasUI != null && camaraCajero != null)
        {
            canvasUI.worldCamera = camaraCajero;
        }
    }

    // Llama a esta función cuando el jugador vuelve a la Vista Principal
    public void VolverAVistaPrincipal()
    {
        camaraCajero.gameObject.SetActive(false);
        mainCamera.gameObject.SetActive(true);

        if (canvasUI != null && mainCamera != null)
        {
            canvasUI.worldCamera = mainCamera;
        }
    }
}