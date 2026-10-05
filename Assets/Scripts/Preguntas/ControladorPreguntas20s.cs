using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ControladorPreguntas20sUI : MonoBehaviour
{
    [Header("Panel Principal")]
    [SerializeField] private GameObject panelPregunta;
    [SerializeField] private GameObject botonIrAEntradaCliente2;
    [SerializeField] private TextMeshProUGUI textoEnunciado;

    [Header("Botones de Opciones")]
    [SerializeField] private Button[] botonesOpciones;
    [SerializeField] private TextMeshProUGUI[] textosOpciones;

    [Header("Referencias Externas")]
    [SerializeField] private DialogoClienteUI dialogoCliente;

    [Header("Temporizador Exclusivo (20 Segundos)")]
    [SerializeField] private float tiempoMaximo = 20f; // Exclusivo de 20s
    [SerializeField] private TextMeshProUGUI textoTemporizador;
    private float tiempoActual;
    private bool temporizadorActivo = false;

    [Header("Pantalla de Derrota (GameOver)")]
    [SerializeField] private GameObject panelGameOver;

    private PreguntaSO preguntaActual;
    private PreguntaSO[] preguntasClienteActual;
    private int indicePreguntaActual = 0;

    private void Start()
    {
        if (panelPregunta != null) panelPregunta.SetActive(false);
        if (botonIrAEntradaCliente2 != null) botonIrAEntradaCliente2.SetActive(false);
        if (panelGameOver != null) panelGameOver.SetActive(false);
    }

    private void Update()
    {
        if (!temporizadorActivo) return;

        if (tiempoActual > 0)
        {
            tiempoActual -= Time.deltaTime;

            if (textoTemporizador != null)
            {
                textoTemporizador.text = Mathf.Ceil(tiempoActual).ToString() + "s";
            }
        }
        else
        {
            tiempoActual = 0;
            temporizadorActivo = false;
            MostrarDerrota();
        }
    }

    public void MostrarPreguntas(PreguntaSO[] listaPreguntas)
    {
        if (listaPreguntas == null || listaPreguntas.Length == 0)
        {
            Debug.LogWarning("El cliente no entregó ninguna pregunta.");
            return;
        }

        preguntasClienteActual = listaPreguntas;
        indicePreguntaActual = 0;

        // El temporizador de 20s arranca al iniciar el bloque de preguntas
        tiempoActual = tiempoMaximo;
        temporizadorActivo = true;

        CargarPreguntaActual();
    }

    private void CargarPreguntaActual()
    {
        preguntaActual = preguntasClienteActual[indicePreguntaActual];

        if (panelPregunta != null) panelPregunta.SetActive(true);
        if (textoEnunciado != null) textoEnunciado.text = preguntaActual.enunciado;

        if (textosOpciones.Length > 0) textosOpciones[0].text = "A) " + preguntaActual.opcionA;
        if (textosOpciones.Length > 1) textosOpciones[1].text = "B) " + preguntaActual.opcionB;
        if (textosOpciones.Length > 2) textosOpciones[2].text = "C) " + preguntaActual.opcionC;
        if (textosOpciones.Length > 3) textosOpciones[3].text = "D) " + preguntaActual.opcionD;

        for (int i = 0; i < botonesOpciones.Length; i++)
        {
            int indice = i;
            botonesOpciones[i].onClick.RemoveAllListeners();
            botonesOpciones[i].onClick.AddListener(() => Responder(indice));
        }
    }

    public void Responder(int indiceRespuesta)
    {
        if (indiceRespuesta == preguntaActual.indiceCorrecto)
        {
            indicePreguntaActual++;

            if (indicePreguntaActual < preguntasClienteActual.Length)
            {
                // Pregunta intermedia: Ocultamos el panel pero EL TEMPORIZADOR DE 20S SIGUE CORRIENDO
                if (panelPregunta != null) panelPregunta.SetActive(false);
                StartCoroutine(EsperarYMostrarSiguientePregunta());
            }
            else
            {
                // ¡Última pregunta acertada! AQUÍ SE DETIENE EL TEMPORIZADOR DE 20S
                temporizadorActivo = false;

                if (dialogoCliente != null)
                {
                    dialogoCliente.ReaccionarARespuesta(preguntaActual.dialogoCorrecto, preguntaActual.animacionCorrecto);
                }

                if (panelPregunta != null) panelPregunta.SetActive(false);
                StartCoroutine(TransicionSiguienteCliente());
            }
        }
        else
        {
            // Si se equivoca, se frena el tiempo y pierde
            temporizadorActivo = false;

            if (dialogoCliente != null)
            {
                dialogoCliente.ReaccionarARespuesta(preguntaActual.dialogoIncorrecto, preguntaActual.animacionIncorrecto);
            }

            if (panelPregunta != null) panelPregunta.SetActive(false);
            StartCoroutine(EsperarYMostrarDerrota());
        }
    }

    private IEnumerator EsperarYMostrarSiguientePregunta()
    {
        yield return new WaitForSeconds(0.5f);
        CargarPreguntaActual();
    }

    private IEnumerator TransicionSiguienteCliente()
    {
        yield return new WaitForSeconds(3.5f);
        if (botonIrAEntradaCliente2 != null) botonIrAEntradaCliente2.SetActive(true);
    }

    private IEnumerator EsperarYMostrarDerrota()
    {
        yield return new WaitForSeconds(2f);
        MostrarDerrota();
    }

    public void MostrarDerrota()
    {
        temporizadorActivo = false;

        if (panelPregunta != null) panelPregunta.SetActive(false);

        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
        }
    }

    public void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}