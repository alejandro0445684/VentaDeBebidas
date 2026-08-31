using UnityEngine;
using UnityEngine.UI;

namespace VentaDeBebidas.UI
{
    public class OptionsUI : MonoBehaviour
    {
        [SerializeField] private Slider volumeSlider;
        [SerializeField] private Toggle fullscreenToggle;

        private const string VolumeKey = "option_volume";
        private const string FullscreenKey = "option_fullscreen";

        private void Start()
        {
            float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
            bool savedFullscreen = PlayerPrefs.GetInt(FullscreenKey, Screen.fullScreen ? 1 : 0) == 1;

            AudioListener.volume = savedVolume;
            Screen.fullScreen = savedFullscreen;

            if (volumeSlider != null)
            {
                volumeSlider.value = savedVolume;
                volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
            }

            if (fullscreenToggle != null)
            {
                fullscreenToggle.isOn = savedFullscreen;
                fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
            }
        }

        public void OnVolumeChanged(float value)
        {
            AudioListener.volume = value;
            PlayerPrefs.SetFloat(VolumeKey, value);
            PlayerPrefs.Save();
        }

        public void OnFullscreenChanged(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
            PlayerPrefs.SetInt(FullscreenKey, isFullscreen ? 1 : 0);
            PlayerPrefs.Save();
        }

        public void ClosePanel()
        {
            gameObject.SetActive(false);
        }
    }
}