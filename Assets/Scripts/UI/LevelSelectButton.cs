using UnityEngine;
using UnityEngine.UI;
using VentaDeBebidas.Data;

namespace VentaDeBebidas.UI
{
    public class LevelSelectButton : MonoBehaviour
    {
        [SerializeField] private Button button;
        [SerializeField] private Text nameLabel;
        [SerializeField] private Text infoLabel;
        [SerializeField] private GameObject lockedOverlay;
        [SerializeField] private GameObject[] starIcons;

        public void Setup(LevelData level, bool unlocked, int stars, System.Action onClick)
        {
            nameLabel.text = level.displayName;
            infoLabel.text = $"{level.difficulty} · {level.role}";

            lockedOverlay.SetActive(!unlocked);
            button.interactable = unlocked;

            for (int i = 0; i < starIcons.Length; i++)
                starIcons[i].SetActive(i < stars);

            button.onClick.RemoveAllListeners();
            if (unlocked)
                button.onClick.AddListener(() => onClick?.Invoke());
        }
    }
}