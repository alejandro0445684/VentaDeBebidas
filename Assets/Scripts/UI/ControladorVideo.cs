using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class ControladorVideo : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    [Header("Nombre exacto de la escena a la que regresa")]
    [SerializeField] private string escenaDestino = "MainMenu"; // Asegúrate de que se llame exactamente así

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();

        if (videoPlayer != null)
        {
            // Nos suscribimos al evento del final del vídeo
            videoPlayer.loopPointReached += RegresarAEscena;
        }
        else
        {
            Debug.LogError("No se encontró el componente VideoPlayer en este objeto.");
        }
    }

    void RegresarAEscena(VideoPlayer vp)
    {
        Debug.Log("El vídeo ha terminado. Intentando cargar la escena: " + escenaDestino);
        SceneManager.LoadScene(escenaDestino);
    }
}