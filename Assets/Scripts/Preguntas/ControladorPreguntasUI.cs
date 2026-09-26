using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ControladorPreguntasUI : MonoBehaviour
{
    [Header("Panel Principal")]
    [SerializeField] private GameObject panelPregunta;
    [SerializeField] private TextMeshProUGUI textoEnunciado;

    [Header("Botones de Opciones")]
    [SerializeField] private Button[] botonesOpciones; // Deben ser 4 botones
    [SerializeField] private TextMeshProUGUI[] textosOpciones; // Los 4 textos de los botones

    [Header("Referencias externas")]
    [SerializeField] private DialogoClienteUI dialogoCliente;

    private PreguntaSO preguntaActual;

    private void Start()
    {
        // Al iniciar el juego, nos aseguramos de que el panel esté oculto
        if (panelPregunta != null)
            panelPregunta.SetActive(false);
    }

    // Esta función la llamará el diálogo del cliente al terminar de hablar
    public void MostrarPregunta(PreguntaSO nuevaPregunta)
    {
        if (nuevaPregunta == null) return;

        preguntaActual = nuevaPregunta;
        panelPregunta.SetActive(true);
        textoEnunciado.ForceMeshUpdate();

        // 1. Asignamos los textos
        textoEnunciado.text = preguntaActual.enunciado;
        textosOpciones[0].text = "A) " + preguntaActual.opcionA;
        textosOpciones[1].text = "B) " + preguntaActual.opcionB;
        textosOpciones[2].text = "C) " + preguntaActual.opcionC;
        textosOpciones[3].text = "D) " + preguntaActual.opcionD;

        // 2. Asignamos la acción a cada botón automáticamente
        for (int i = 0; i < botonesOpciones.Length; i++)
        {
            int indice = i; // Guardamos la variable local para el click
            botonesOpciones[i].onClick.RemoveAllListeners();
            botonesOpciones[i].onClick.AddListener(() => Responder(indice));
        }
    }

    // Comprueba si la opción presionada es la correcta
    public void Responder(int indiceSeleccionado)
    {
        if (indiceSeleccionado == preguntaActual.indiceCorrecto)
        {
            Debug.Log("¡RESPUESTA CORRECTA! El cliente pagó correctamente.");
            // Aquí podrás sumar dinero, reproducir sonido de éxito, etc.
        // Si la respuesta es correcta, hacemos hablar al cliente
            if (dialogoCliente != null)
            {
                dialogoCliente.ReaccionarARespuesta(preguntaActual.dialogoCorrecto, preguntaActual.animacionCorrecto);
            }
        }
        else
        {
            Debug.Log("RESPUESTA INCORRECTA! El cliente se quejó.");
            
            // Si es incorrecta, dice el diálogo de queja/error
            if (dialogoCliente != null)
            {
                dialogoCliente.ReaccionarARespuesta(preguntaActual.dialogoIncorrecto, preguntaActual.animacionIncorrecto);
            }
        }

        // Ocultamos el panel tras responder
         panelPregunta.SetActive(false);
    }
    
}