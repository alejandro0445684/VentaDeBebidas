using UnityEngine;

[CreateAssetMenu(fileName = "NuevaPregunta", menuName = "JuegoCajero/Pregunta")]
public class PreguntaSO : ScriptableObject
{
    [TextArea(2, 5)]
    public string enunciado; // Ejemplo: "¿Cuánto cuesta esto?"

    [Header("Opciones de Respuesta")]
    public string opcionA;
    public string opcionB;
    public string opcionC;
    public string opcionD;

    [Header("Respuesta Correcta")]
    [Tooltip("Selecciona cuál es la respuesta correcta: 0 = A, 1 = B, 2 = C, 3 = D")]
    public int indiceCorrecto;

    [Header("Diálogos de Respuesta")]
    public string dialogoCorrecto = "¡Muchas gracias! Es justo lo que buscaba.";
    public string dialogoIncorrecto = "Hm... creo que ese no es el precio correcto.";

    [Header("Animaciones de Respuesta (Opcional)")]
    public string animacionCorrecto = "Hablar"; // O el nombre del estado/trigger para festejar/agradecer
    public string animacionIncorrecto = "Hablar"; // O el nombre del estado/trigger de molestia/duda
}