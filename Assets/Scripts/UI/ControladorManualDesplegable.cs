using UnityEngine;

public class ControladorManualDesplegable : MonoBehaviour
{
    [Header("Referencia al Manual")]
    [SerializeField] private GameObject manualDesplegable;

    private void OnEnable()
    {
        CerrarManual();
    }
    // Abre el manual de precios
    public void AbrirManual()
    {
        if (manualDesplegable != null)
        {
            manualDesplegable.SetActive(true);
        }
    }

    // Cierra el manual de precios
    public void CerrarManual()
    {
        if (manualDesplegable != null)
        {
            manualDesplegable.SetActive(false);
        }
    }

    // Abre o cierra alternadamente al tocar el mismo botón
    public void AlternarManual()
    {
        if (manualDesplegable != null)
        {
            manualDesplegable.SetActive(!manualDesplegable.activeSelf);
        }
    }
}