using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControladorPreguntasUI : MonoBehaviour
{
    [Header("Panel Principal")]
    [SerializeField] private GameObject panelPregunta;
    [SerializeField] private GameObject botonIrAEntradaCliente2;
    [SerializeField] private TextMeshProUGUI textoEnunciado;

    [Header("Botones de Opciones")]
    [SerializeField] private Button[] botonesOpciones;
    [SerializeField] private TextMeshProUGUI[] textosOpciones;

    [Header("Referencias externas")]
    [SerializeField] private DialogoClienteUI dialogoCliente;

    private PreguntaSO preguntaActual;

    private void Start()
    {
        if (panelPregunta != null) panelPregunta.SetActive(false);
        if (botonIrAEntradaCliente2 != null) botonIrAEntradaCliente2.SetActive(false);
    }

    // Esta función recibe la pregunta específica que le mande el cliente
    public void MostrarPregunta(PreguntaSO preguntaDelCliente)
    {
        if (preguntaDelCliente == null)
        {
            Debug.LogWarning("El cliente no entregó ninguna pregunta.");
            return;
        }

        preguntaActual = preguntaDelCliente;
        panelPregunta.SetActive(true);
        textoEnunciado.ForceMeshUpdate();

        // Asignamos el enunciado y las opciones
        textoEnunciado.text = preguntaActual.enunciado;
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

    public void Responder(int indiceSeleccionado)
    {
        if (preguntaActual == null) return;

        if (indiceSeleccionado == preguntaActual.indiceCorrecto)
        {
            if (dialogoCliente != null)
                dialogoCliente.ReaccionarARespuesta(preguntaActual.dialogoCorrecto, preguntaActual.animacionCorrecto);

            if (panelPregunta != null) panelPregunta.SetActive(false);
            StartCoroutine(TransicionSiguienteCliente());
        }
        else
        {
            if (dialogoCliente != null)
                dialogoCliente.ReaccionarARespuesta(preguntaActual.dialogoIncorrecto, preguntaActual.animacionIncorrecto);

            if (panelPregunta != null) panelPregunta.SetActive(false);
        }
    }

    private IEnumerator TransicionSiguienteCliente()
    {
        yield return new WaitForSeconds(3.5f);
        if (botonIrAEntradaCliente2 != null) botonIrAEntradaCliente2.SetActive(true);
    }
}