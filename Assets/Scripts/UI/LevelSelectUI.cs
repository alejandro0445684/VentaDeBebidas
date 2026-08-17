using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using VentaDeBebidas.Data;
using VentaDeBebidas.Managers;

namespace VentaDeBebidas.UI
{
    public class LevelSelectUI : MonoBehaviour
    {
        [SerializeField] private Transform levelButtonsContainer;
        [SerializeField] private LevelSelectButton levelButtonPrefab;

        private void Start()
        {
            PopulateLevels();
        }

        private void PopulateLevels()
        {
            foreach (Transform child in levelButtonsContainer)
                Destroy(child.gameObject);

            var levels = LevelDatabase.GetAllLevels();

            for (int i = 0; i < levels.Count; i++)
            {
                var level = levels[i];
                var button = Instantiate(levelButtonPrefab, levelButtonsContainer);

                bool unlocked = GameManager.Instance == null || GameManager.Instance.IsLevelUnlocked(i);
                int stars = GameManager.Instance != null ? GameManager.Instance.GetStars(level.levelId) : 0;

                button.Setup(level, unlocked, stars, () => OnLevelSelected(level));
            }
        }

        private void OnLevelSelected(LevelData level)
        {
            if (GameManager.Instance != null)
            {
                GameManager.Instance.SelectLevel(level);
            }
            SceneManager.LoadScene("Gameplay");
        }

        public void OnBackPressed()
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}