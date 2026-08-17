using UnityEngine;
using VentaDeBebidas.Data;

namespace VentaDeBebidas.Gameplay
{
    /// <summary>
    /// Lleva el puntaje (0-100) y los errores durante un nivel, y calcula
    /// cuántas estrellas corresponden al terminar.
    /// </summary>
    public class ScoreSystem : MonoBehaviour
    {
        [SerializeField] private int totalTasks = 1; // total de clientes o de ítems a comprar
        private int _completedTasks;
        private int _errors;
        private bool _finishedWithinTime = true;

        public int Errors => _errors;
        public int Score { get; private set; }

        public void Init(int total)
        {
            totalTasks = Mathf.Max(1, total);
            _completedTasks = 0;
            _errors = 0;
            _finishedWithinTime = true;
            Score = 0;
        }

        public void RegisterSuccess()
        {
            _completedTasks++;
            Recalculate();
        }

        public void RegisterError()
        {
            _errors++;
            Recalculate();
        }

        public void MarkTimeExceeded()
        {
            _finishedWithinTime = false;
            Recalculate();
        }

        private void Recalculate()
        {
            float completionRatio = (float)_completedTasks / totalTasks;
            float errorPenalty = Mathf.Clamp01(_errors * 0.1f); // -10% por error, hasta 100%
            Score = Mathf.RoundToInt(Mathf.Clamp01(completionRatio - errorPenalty) * 100f);
        }

        public int CalculateStars(LevelData level)
        {
            if (_completedTasks < totalTasks) return 0; // no completó, sin estrellas
            if (!_finishedWithinTime) return 1;
            if (Score >= level.threeStarScore && _errors == 0) return 3;
            if (Score >= level.twoStarScore) return 2;
            return 1;
        }
    }
}
