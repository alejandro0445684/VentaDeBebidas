using UnityEngine;

namespace VentaDeBebidas.Managers
{
    /// <summary>
    /// Singleton simple para reproducir efectos de sonido comunes.
    /// Colocar en la escena MainMenu junto al GameManager (también persiste).
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField] private AudioSource sfxSource; // TODO: asignar en el Inspector
        [SerializeField] private AudioClip successClip;  // TODO: asignar en el Inspector
        [SerializeField] private AudioClip errorClip;      // TODO: asignar en el Inspector
        [SerializeField] private AudioClip coinClip;        // TODO: asignar en el Inspector
        [SerializeField] private AudioClip uiClickClip;      // TODO: asignar en el Inspector

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        public void PlaySuccess() => Play(successClip);
        public void PlayError() => Play(errorClip);
        public void PlayCoin() => Play(coinClip);
        public void PlayUiClick() => Play(uiClickClip);

        private void Play(AudioClip clip)
        {
            if (clip == null || sfxSource == null) return;
            sfxSource.PlayOneShot(clip);
        }
    }
}
