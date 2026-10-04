using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ControladorPreguntasUI : MonoBehaviour
{
    [Header("Panel Principal")]
    [SerializeField] private GameObject panelPregunta; //[span_2](start_span)[span_2](end_span)[span_3](start_span)[span_3](end_span)
    [SerializeField] private GameObject botonIrAEntradaCliente2; //[span_4](start_span)[span_4](end_span)[span_5](start_span)[span_5](end_span)
    [SerializeField] private TextMeshProUGUI textoEnunciado; //[span_6](start_span)[span_6](end_span)[span_7](start_span)[span_7](end_span)

    [Header("Botones de Opciones")]
    [SerializeField] private Button[] botonesOpciones; //[span_8](start_span)[span_8](end_span)[span_9](start_span)[span_9](end_span)
    [SerializeField] private TextMeshProUGUI[] textosOpciones; //[span_10](start_span)[span_10](end_span)[span_11](start_span)[span_11](end_span)

    [Header("Referencias externas")]
    [SerializeField] private DialogoClienteUI dialogoCliente; //[span_12](start_span)[span_12](end_span)[span_13](start_span)[span_13](end_span)

    [Header("Configuración de Temporizador")]
    [SerializeField] private float tiempoMaximo = 30f; // Tiempo limite en segundos
    [SerializeField] private TextMeshProUGUI textoTemporizador; // El texto en pantalla para ver los segundos
    private float tiempoActual;
    private bool temporizadorActivo = false;

    [Header("Pantalla de Derrota (GameOver)")]
    [SerializeField] private GameObject panelGameOver; // Arrastra aquí el panel de Level Failed

    private PreguntaSO preguntaActual; //[span_14](start_span)[span_14](end_span)[span_15](start_span)[span_15](end_span)

    private void Start()
    {
        if (panelPregunta != null) panelPregunta.SetActive(false); //[span_16](start_span)[span_16](end_span)
        if (botonIrAEntradaCliente2 != null) botonIrAEntradaCliente2.SetActive(false); //[span_17](start_span)[span_17](end_span)
        if (panelGameOver != null) panelGameOver.SetActive(false); // Nos aseguramos de ocultarlo al inicio
    }

    private void Update()
    {
        if (!temporizadorActivo) return;

        if (tiempoActual > 0)
        {
            tiempoActual -= Time.deltaTime;

            // Actualiza los segundos visibles en la interfaz
            if (textoTemporizador != null)
            {
                textoTemporizador.text = Mathf.Ceil(tiempoActual).ToString() + "s";
            }
        }
        else
        {
            // Se agotó el tiempo: pierde el nivel
            tiempoActual = 0;
            temporizadorActivo = false;
            MostrarDerrota();
        }
    }

    public void MostrarPregunta(PreguntaSO preguntaDelCliente)
    {
        if (preguntaDelCliente == null) //[span_18](start_span)[span_18](end_span)
        {
            Debug.LogWarning("El cliente no entregó ninguna pregunta."); //[span_19](start_span)[span_19](end_span)
            return; //[span_20](start_span)[span_20](end_span)
        }

        preguntaActual = preguntaDelCliente; //[span_21](start_span)[span_21](end_span)
        panelPregunta.SetActive(true); //[span_22](start_span)[span_22](end_span)
        textoEnunciado.ForceMeshUpdate(); //[span_23](start_span)[span_23](end_span)

        // Asignamos el enunciado y las opciones de respuesta
        textoEnunciado.text = preguntaActual.enunciado; //[span_24](start_span)[span_24](end_span)
        if (textosOpciones.Length > 0) textosOpciones[0].text = "A) " + preguntaActual.opcionA; //[span_25](start_span)[span_25](end_span)
        if (textosOpciones.Length > 1) textosOpciones[1].text = "B) " + preguntaActual.opcionB; //[span_26](start_span)[span_26](end_span)
        if (textosOpciones.Length > 2) textosOpciones[2].text = "C) " + preguntaActual.opcionC; //[span_27](start_span)[span_27](end_span)
        if (textosOpciones.Length > 3) textosOpciones[3].text = "D) " + preguntaActual.opcionD; //[span_28](start_span)[span_28](end_span)

        for (int i = 0; i < botonesOpciones.Length; i++) //[span_29](start_span)[span_29](end_span)
        {
            int indice = i; //[span_30](start_span)[span_30](end_span)
            botonesOpciones[i].onClick.RemoveAllListeners(); //[span_31](start_span)[span_31](end_span)
            botonesOpciones[i].onClick.AddListener(() => Responder(indice)); //[span_32](start_span)[span_32](end_span)
        }

        // Iniciar la cuenta regresiva al abrir la pregunta
        tiempoActual = tiempoMaximo;
        temporizadorActivo = true;
    }

    public void Responder(int indiceSeleccionado)
    {
        if (preguntaActual == null) return; //[span_33](start_span)[span_33](end_span)

        // Detenemos el tiempo apenas el jugador presiona un botón
        temporizadorActivo = false;

        if (indiceSeleccionado == preguntaActual.indiceCorrecto) //[span_34](start_span)[span_34](end_span)
        {
            // RESPUESTA CORRECTA
            if (dialogoCliente != null) //[span_35](start_span)[span_35](end_span)
                dialogoCliente.ReaccionarARespuesta(preguntaActual.dialogoCorrecto, preguntaActual.animacionCorrecto); //[span_36](start_span)[span_36](end_span)

            if (panelPregunta != null) panelPregunta.SetActive(false); //[span_37](start_span)[span_37](end_span)
            StartCoroutine(TransicionSiguienteCliente()); //[span_38](start_span)[span_38](end_span)
        }
        else
        {
            // RESPUESTA INCORRECTA
            if (dialogoCliente != null) //[span_39](start_span)[span_39](end_span)
                dialogoCliente.ReaccionarARespuesta(preguntaActual.dialogoIncorrecto, preguntaActual.animacionIncorrecto); //[span_40](start_span)[span_40](end_span)
            
            if (panelPregunta != null) panelPregunta.SetActive(false); //[span_41](start_span)[span_41](end_span)

            // Muestra la pantalla de derrota tras 2 segundos para permitir ver la reacción del cliente
            StartCoroutine(EsperarYMostrarDerrota());
        }
    }

    private IEnumerator TransicionSiguienteCliente()
    {
        yield return new WaitForSeconds(3.5f); //[span_42](start_span)[span_42](end_span)
        if (botonIrAEntradaCliente2 != null) botonIrAEntradaCliente2.SetActive(true); //[span_43](start_span)[span_43](end_span)
    }

    private IEnumerator EsperarYMostrarDerrota()
    {
        yield return new WaitForSeconds(2f);
        MostrarDerrota();
    }

    public void MostrarDerrota()
    {
        temporizadorActivo = false;

        // Ocultamos el panel de la pregunta si sigue activo
        if (panelPregunta != null) panelPregunta.SetActive(false);

        // Activamos el panel de Level Failed
        if (panelGameOver != null)
        {
            panelGameOver.SetActive(true);
        }
    }

    public void ReiniciarNivel()
    {
        //Recarga la escena actual en la que estas jugando//
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}