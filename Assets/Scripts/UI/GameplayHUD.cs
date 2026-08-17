using UnityEngine;
using UnityEngine.UI;
using VentaDeBebidas.Data;

namespace VentaDeBebidas.UI
{
    /// <summary>
    /// HUD durante la partida: dinero (caja o presupuesto), cronómetro
    /// (si el nivel tiene tiempo límite) y nombre del nivel. Suscribí
    /// los eventos de CashierController/BuyerController a UpdateMoney
    /// desde el Inspector o desde LevelManager.
    /// </summary>
    public class GameplayHUD : MonoBehaviour
    {
        [SerializeField] private Text levelNameLabel; // TODO: asignar (o TMP_Text)
        [SerializeField] private Text moneyLabel;      // TODO: asignar (o TMP_Text)
        [SerializeField] private Text timerLabel;       // TODO: asignar (o TMP_Text)
        [SerializeField] private GameObject timerContainer; // TODO: asignar en el Inspector

        public void Init(LevelData level)
        {
            levelNameLabel.text = level.displayName;
            UpdateMoney(level.startingMoney);

            bool hasTimeLimit = level.globalTimeLimitSeconds > 0f;
            timerContainer.SetActive(hasTimeLimit);
            if (hasTimeLimit) UpdateTimer(level.globalTimeLimitSeconds);
        }

        public void UpdateMoney(int amount)
        {
            moneyLabel.text = $"₲ {amount:N0}";
        }

        public void UpdateTimer(float secondsRemaining)
        {
            int s = Mathf.Max(0, Mathf.CeilToInt(secondsRemaining));
            timerLabel.text = $"{s / 60:00}:{s % 60:00}";
        }
    }
}
