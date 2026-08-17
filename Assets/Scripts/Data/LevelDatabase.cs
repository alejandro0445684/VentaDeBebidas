using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace VentaDeBebidas.Data
{
    /// <summary>
    /// Carga automáticamente todos los LevelData desde
    /// Assets/Resources/Levels y los expone ordenados por nombre
    /// (Level_01, Level_02, ... Level_10) para el menú de selección
    /// y el LevelManager.
    /// </summary>
    public static class LevelDatabase
    {
        private static List<LevelData> _cachedLevels;

        public static List<LevelData> GetAllLevels()
        {
            if (_cachedLevels != null) return _cachedLevels;

            var loaded = Resources.LoadAll<LevelData>("Levels");
            _cachedLevels = loaded
                .OrderBy(l => l.levelId)
                .ToList();

            if (_cachedLevels.Count < 10)
            {
                Debug.LogWarning(
                    $"LevelDatabase: se encontraron {_cachedLevels.Count} niveles en " +
                    "Assets/Resources/Levels, pero el diseño pide un mínimo de 10. " +
                    "Creá los assets faltantes (Level_01 ... Level_10) desde " +
                    "Assets > Create > VentaDeBebidas > Level Data.");
            }

            return _cachedLevels;
        }

        public static LevelData GetLevelByIndex(int index)
        {
            var levels = GetAllLevels();
            if (index < 0 || index >= levels.Count)
            {
                Debug.LogError($"LevelDatabase: índice de nivel inválido ({index}).");
                return null;
            }
            return levels[index];
        }

        public static LevelData GetLevelById(string levelId)
        {
            return GetAllLevels().FirstOrDefault(l => l.levelId == levelId);
        }

        /// <summary>Fuerza a recargar desde disco (útil tras editar assets en el Editor).</summary>
        public static void ClearCache() => _cachedLevels = null;
    }
}
