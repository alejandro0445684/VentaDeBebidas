using UnityEngine;
using UnityEngine.SceneManagement;
using VentaDeBebidas.Data;
using VentaDeBebidas.Gameplay;

namespace VentaDeBebidas.Managers
{
    /// <summary>
    /// Vive en la escena "Gameplay". Al iniciar, lee GameManager.selectedLevel
    /// y activa el flujo correspondiente (Cajero o Comprador), controla el
    /// tiempo límite global (si el nivel lo tiene) y dispara la pantalla de
    /// resultados al terminar.
    /// </summary>
    public class LevelManager : MonoBehaviour
    {
        [Header("Referencias de rol (activar solo la que corresponda)")]
        [SerializeField] private GameObject cashierRoot;   // TODO: asignar en el Inspector
        [SerializeField] private GameObject buyerRoot;     // TODO: asignar en el Inspector
        [SerializeField] private CashierController cashierController; // TODO: asignar en el Inspector
        [SerializeField] private BuyerController buyerController;     // TODO: asignar en el Inspector
        [SerializeField] private ShopSystem shopSystem;                // TODO: asignar en el Inspector
        [SerializeField] private ScoreSystem scoreSystem;               // TODO: asignar en el Inspector

        [Header("UI")]
        [SerializeField] private UI.GameplayHUD hud;         // TODO: asignar en el Inspector
        [SerializeField] private UI.ResultsUI resultsUI;     // TODO: asignar en el Inspector

        private LevelData _level;
        private float _remainingTime;
        private bool _timeLimited;
        private bool _finished;

        private void Start()
        {
            _level = GameManager.Instance != null ? GameManager.Instance.selectedLevel : null;

            if (_level == null)
            {
                Debug.LogError("LevelManager: no hay nivel seleccionado. Volviendo al menú.");
                SceneManager.LoadScene("MainMenu");
                return;
            }

            shopSystem.Init(_level);

            _timeLimited = _level.globalTimeLimitSeconds > 0f;
            _remainingTime = _level.globalTimeLimitSeconds;

            bool isCashier = _level.role == LevelRole.Cajero;
            cashierRoot.SetActive(isCashier);
            buyerRoot.SetActive(!isCashier);

            if (isCashier)
            {
                cashierController.OnLevelFinished += HandleLevelFinished;
                cashierController.StartLevel(_level);
            }
            else
            {
                buyerController.OnLevelFinished += HandleLevelFinished;
                buyerController.StartLevel(_level);
            }

            hud.Init(_level);
        }

        private void Update()
        {
            if (_finished || !_timeLimited) return;

            _remainingTime -= Time.deltaTime;
            hud.UpdateTimer(_remainingTime);

            if (_remainingTime <= 0f)
            {
                scoreSystem.MarkTimeExceeded();
                HandleLevelFinished();
            }
        }

        private void HandleLevelFinished()
        {
            if (_finished) return;
            _finished = true;

            int stars = scoreSystem.CalculateStars(_level);
            GameManager.Instance.SetStars(_level.levelId, stars);

            resultsUI.Show(_level, scoreSystem.Score, stars, scoreSystem.Errors);
        }

        public void GoToLevelSelect()
        {
            SceneManager.LoadScene("LevelSelect");
        }

        public void RetryLevel()
        {
            SceneManager.LoadScene("Gameplay");
        }
    }
}
