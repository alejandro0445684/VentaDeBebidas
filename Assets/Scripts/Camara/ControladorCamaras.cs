using UnityEngine;

public class ControladorCamaras : MonoBehaviour
{
    [Header("Cámaras del Juego")]
    [SerializeField] private GameObject camaraPrincipal;
    [SerializeField] private GameObject camaraCajero;
    [SerializeField] private GameObject camara2doCliente;

    [Header("Canvas de UI")]
    [SerializeField] private GameObject canvasJuego;
    [SerializeField] private GameObject canvasCajero;
    [SerializeField] private GameObject canvasCliente2;
    [SerializeField] private GameObject canvasCajero2;

    [Header("Elementos del Cajero")]
    [SerializeField] private GameObject panelVistaCajero;

    // --- VISTA CAJERO (CLIENTE 1) ---
    public void ActivarVistaCajero()
    {
        // 1. Alternar Cámaras
        if (camaraPrincipal != null && camaraCajero != null)
        {
            camaraPrincipal.SetActive(false);
            if (camara2doCliente != null) camara2doCliente.SetActive(false);
            camaraCajero.SetActive(true);
        }

        // 2. Control de Canvas
        if (canvasJuego != null) canvasJuego.SetActive(false);
        if (canvasCliente2 != null) canvasCliente2.SetActive(false);
        if (canvasCajero2 != null) canvasCajero2.SetActive(false);
        if (canvasCajero != null) canvasCajero.SetActive(true);

        // 3. Activar Panel
        if (panelVistaCajero != null) panelVistaCajero.SetActive(true);
    }

    // --- VOLVER A VISTA GENERAL ---
    public void VolverAVistaGeneral()
    {
        if (camaraPrincipal != null) camaraPrincipal.SetActive(true);
        if (camaraCajero != null) camaraCajero.SetActive(false);
        if (camara2doCliente != null) camara2doCliente.SetActive(false);

        if (canvasJuego != null) canvasJuego.SetActive(true);
        if (canvasCajero != null) canvasCajero.SetActive(false);
        if (canvasCliente2 != null) canvasCliente2.SetActive(false);
        if (canvasCajero2 != null) canvasCajero2.SetActive(false);
    }

    // --- VISTA CLIENTE 2 ---
    public void ActivarVistaCliente2()
    {
        if (camaraPrincipal != null) camaraPrincipal.SetActive(false);
        if (camaraCajero != null) camaraCajero.SetActive(false);
        if (camara2doCliente != null) camara2doCliente.SetActive(true);

        if (canvasJuego != null) canvasJuego.SetActive(false);
        if (canvasCajero != null) canvasCajero.SetActive(false);
        if (canvasCajero2 != null) canvasCajero2.SetActive(false);
        if (canvasCliente2 != null) canvasCliente2.SetActive(true);
    }

    // --- VISTA CAJERO CLIENTE 2 ---
    public void ActivarVistaCajeroCliente2()
    {
        if (camaraPrincipal != null) camaraPrincipal.SetActive(false);
        if (camara2doCliente != null) camara2doCliente.SetActive(false);
        if (camaraCajero != null) camaraCajero.SetActive(true);

        if (canvasJuego != null) canvasJuego.SetActive(false);
        if (canvasCajero != null) canvasCajero.SetActive(false);
        if (canvasCliente2 != null) canvasCliente2.SetActive(false);
        if (canvasCajero2 != null) canvasCajero2.SetActive(true);

        if (panelVistaCajero != null) panelVistaCajero.SetActive(true);
    }
}
