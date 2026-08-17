using UnityEngine;
using UnityEngine.SceneManagement;

namespace VentaDeBebidas.UI
{
    /// <summary>
    /// Controla los botones del menú principal.
    /// Colgar de un Canvas en la escena "MainMenu" con botones:
    /// Jugar (PlayButton), Salir (QuitButton).
    /// Conectar cada botón a estos métodos desde el Inspector (OnClick).
    /// </summary>
    public class MainMenuUI : MonoBehaviour
    {
        public void OnPlayPressed()
        {
            SceneManager.LoadScene("LevelSelect");
        }

        public void OnQuitPressed()
        {
            Debug.Log("Saliendo del juego...");
            Application.Quit();
        }
    }
}
