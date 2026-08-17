using System.Collections.Generic;
using UnityEngine;
using VentaDeBebidas.Data;

namespace VentaDeBebidas.Managers
{
    /// <summary>
    /// Singleton persistente. Guarda qué nivel se seleccionó, el progreso
    /// (estrellas por nivel) y sobrevive entre escenas.
    /// Colocar un GameObject "GameManager" con este script en la escena
    /// MainMenu (con DontDestroyOnLoad ya se encarga solo).
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Selección actual")]
        public LevelData selectedLevel;

        [Header("Progreso guardado (nivelId -> estrellas 0-3)")]
        private Dictionary<string, int> _levelStars = new Dictionary<string, int>();

        private const string SaveKeyPrefix = "stars_";

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProgress();
        }

        public void SelectLevel(LevelData level)
        {
            selectedLevel = level;
        }

        public int GetStars(string levelId)
        {
            return _levelStars.TryGetValue(levelId, out var stars) ? stars : 0;
        }

        /// <summary>Guarda el mejor puntaje de estrellas obtenido (no baja si ya tenía más).</summary>
        public void SetStars(string levelId, int stars)
        {
            if (!_levelStars.ContainsKey(levelId) || _levelStars[levelId] < stars)
            {
                _levelStars[levelId] = stars;
                PlayerPrefs.SetInt(SaveKeyPrefix + levelId, stars);
                PlayerPrefs.Save();
            }
        }

        /// <summary>Un nivel está desbloqueado si es el primero, o si el anterior tiene al menos 1 estrella.</summary>
        public bool IsLevelUnlocked(int levelIndex)
        {
            if (levelIndex <= 0) return true;
            var levels = LevelDatabase.GetAllLevels();
            if (levelIndex - 1 >= levels.Count) return false;
            var previous = levels[levelIndex - 1];
            return GetStars(previous.levelId) > 0;
        }

        private void LoadProgress()
        {
            foreach (var level in LevelDatabase.GetAllLevels())
            {
                if (PlayerPrefs.HasKey(SaveKeyPrefix + level.levelId))
                {
                    _levelStars[level.levelId] = PlayerPrefs.GetInt(SaveKeyPrefix + level.levelId);
                }
            }
        }
    }
}
