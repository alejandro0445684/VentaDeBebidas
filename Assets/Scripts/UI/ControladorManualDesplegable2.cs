using UnityEngine;

public class ControladorManualDesplegable2 : MonoBehaviour
{
    [Header("Referencia al Manual 2")]
    [SerializeField] private GameObject manualDesplegable2;

    private void OnEnable()
    {
        CerrarManual();
    }

    // Abre el manual de precios del 2do cliente
    public void AbrirManual()
    {
        if (manualDesplegable2 != null)
        {
            manualDesplegable2.SetActive(true);
        }
    }

    // Cierra el manual de precios del 2do cliente
    public void CerrarManual()
    {
        if (manualDesplegable2 != null)
        {
            manualDesplegable2.SetActive(false);
        }
    }

    // Abre o cierra alternadamente al tocar el mismo botón
    public void AlternarManual()
    {
        if (manualDesplegable2 != null)
        {
            manualDesplegable2.SetActive(!manualDesplegable2.activeSelf);
        }
    }
}