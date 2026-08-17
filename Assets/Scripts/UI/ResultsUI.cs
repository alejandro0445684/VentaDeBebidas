using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VentaDeBebidas.Data;

namespace VentaDeBebidas.UI
{
    /// <summary>
    /// Panel de resultados que aparece al terminar un nivel: puntaje,
    /// errores, estrellas obtenidas, y botones Reintentar / Volver al mapa.
    /// Empieza desactivado y se activa desde LevelManager al terminar.
    /// </summary>
    public class ResultsUI : MonoBehaviour
    {
        [SerializeField] private GameObject panelRoot; // TODO: asignar en el Inspector
        [SerializeField] private Text titleLabel;       // TODO: asignar (o TMP_Text)
        [SerializeField] private Text scoreLabel;        // TODO: asignar (o TMP_Text)
        [SerializeField] private Text errorsLabel;        // TODO: asignar (o TMP_Text)
        [SerializeField] private GameObject[] starIcons;   // TODO: asignar 3 iconos de estrella

        public void Show(LevelData level, int score, int stars, int errors)
        {
            panelRoot.SetActive(true);
            titleLabel.text = stars > 0 ? "¡Nivel completado!" : "Nivel no completado";
            scoreLabel.text = $"Puntaje: {score}";
            errorsLabel.text = $"Errores: {errors}";

            for (int i = 0; i < starIcons.Length; i++)
                starIcons[i].SetActive(i < stars);
        }

        public void OnRetryPressed()
        {
            SceneManager.LoadScene("Gameplay");
        }

        public void OnLevelSelectPressed()
        {
            SceneManager.LoadScene("LevelSelect");
        }
    }
}
