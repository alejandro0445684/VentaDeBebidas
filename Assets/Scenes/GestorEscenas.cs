using UnityEngine;
using UnityEngine.SceneManagement;

public class GestorEscenas : MonoBehaviour
{
    // Cargar un nivel por su nombre exacto
    public void CambiarEscena(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }

    // Cargar el siguiente nivel o volver al panel usando el índice actual
    public void IrAEscenaIndice(int indiceEscena)
    {
        SceneManager.LoadScene(indiceEscena);
    }

    // Salir del juego (útil para el menú principal)
    public void SalirJuego()
    {
        Application.Quit();
    }
}