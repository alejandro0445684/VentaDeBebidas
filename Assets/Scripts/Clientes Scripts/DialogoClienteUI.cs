using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogoClienteUI : MonoBehaviour
{
    [Header("Lista de Diálogos Posibles")]
    [TextArea(2, 4)]
    [SerializeField] private List<string> dialogosPosibles = new List<string>()
    {
        "¿Podrías cobrarme por favor?",
        "Esto sería todo.",
        "Hola, cobrarme esto rápido.",
        "Me cobras esto, por favor?"
    };

    [Header("Componentes de UI")]
    [SerializeField] private GameObject globoTextoObjeto;
    [SerializeField] private TextMeshProUGUI textoDialogo;

    [Header("Sistema de Preguntas")]
    [SerializeField] private MonoBehaviour controladorPreguntas;
    [SerializeField] private PreguntaSO[] preguntaDeEsteCliente;

    [Header("Animación del Cliente")]
    [SerializeField] private Animator animatorClienteFrontal;

    [Header("Configuración de Tiempos y Animaciones")]
    [SerializeField] private float duracionMensaje = 3.5f;
    [SerializeField] private string nombreEstadoHablar = "Hablar";
    [SerializeField] private string nombreEstadoIdle = "Idle";

    private void OnEnable()
    {
        IniciarDialogo();
    }

    public void IniciarDialogo()
    {
        if (dialogosPosibles != null && dialogosPosibles.Count > 0)
        {
            int indiceAleatorio = Random.Range(0, dialogosPosibles.Count);
            string fraseElegida = dialogosPosibles[indiceAleatorio];

            StartCoroutine(RutinaHablar(fraseElegida));
        }
    }

    private IEnumerator RutinaHablar(string texto)
    {
        if (textoDialogo != null) textoDialogo.text = texto;
        if (globoTextoObjeto != null) globoTextoObjeto.SetActive(true);

        if (animatorClienteFrontal != null)
        {
            animatorClienteFrontal.Play(nombreEstadoHablar);
        }

        yield return new WaitForSeconds(duracionMensaje);

        if (globoTextoObjeto != null) globoTextoObjeto.SetActive(false);

        if (animatorClienteFrontal != null)
        {
            animatorClienteFrontal.Play(nombreEstadoIdle);
        }

        // Iniciamos la trivia una vez que el cliente termina su saludo inicial
        if (controladorPreguntas != null && preguntaDeEsteCliente != null)
        {
            if (controladorPreguntas is ControladorPreguntas20sUI controlador20)
            {
                controlador20.MostrarPreguntas(preguntaDeEsteCliente);
            }
            else if (controladorPreguntas is ControladorPreguntasUI controlador30)
            {
                controlador30.MostrarPreguntas(preguntaDeEsteCliente);
            }
        }
        
    }

    public void ReaccionarARespuesta(string textoRespuesta, string nombreAnimacion)
    {
        gameObject.SetActive(true);
        StartCoroutine(RutinaReaccion(textoRespuesta, nombreAnimacion));
    }

    private IEnumerator RutinaReaccion(string textoRespuesta, string nombreAnimacion)
    {
        if (textoDialogo != null) textoDialogo.text = textoRespuesta;
        if (globoTextoObjeto != null) globoTextoObjeto.SetActive(true);

        if (animatorClienteFrontal != null && !string.IsNullOrEmpty(nombreAnimacion))
        {
            animatorClienteFrontal.Play(nombreAnimacion);
        }

        yield return new WaitForSeconds(duracionMensaje);

        if (globoTextoObjeto != null) globoTextoObjeto.SetActive(false);
    }
}