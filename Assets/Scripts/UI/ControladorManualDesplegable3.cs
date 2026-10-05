using UnityEngine;

public class ControladorManualDesplegable3 : MonoBehaviour
{
    [Header("Referencia al Manual 3")]
    [SerializeField] private GameObject manualDesplegable3;

    private void OnEnable()
    {
        CerrarManual();
    }

    // Abre el manual de precios del 3er cliente
    public void AbrirManual()
    {
        if (manualDesplegable3 != null)
        {
            manualDesplegable3.SetActive(true);
        }
    }

    // Cierra el manual de precios del 3er cliente
    public void CerrarManual()
    {
        if (manualDesplegable3 != null)
        {
            manualDesplegable3.SetActive(false);
        }
    }

    // Abre o cierra alternadamente al tocar el mismo botón
    public void AlternarManual()
    {
        if (manualDesplegable3 != null)
        {
            manualDesplegable3.SetActive(!manualDesplegable3.activeSelf);
        }
    }
}