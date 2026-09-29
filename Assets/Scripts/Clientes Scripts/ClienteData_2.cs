using System.Collections.Generic;
using UnityEngine;

public class ClienteData_2 : MonoBehaviour
{
    [Header("Preguntas de este cliente")]
    [SerializeField] private List<PreguntaSO> preguntasDelCliente; // Lista de preguntas ÚNICAS para este cliente

    // Función para obtener una pregunta (puede ser la primera o una al azar de sus preguntas)
    public PreguntaSO ObtenerPregunta()
    {
        if (preguntasDelCliente == null || preguntasDelCliente.Count == 0) return null;

        // Si solo tiene 1 pregunta asignada, devuelve esa
        if (preguntasDelCliente.Count == 1) return preguntasDelCliente[0];

        // Si tiene varias, elige una al azar entre sus preguntas
        int indiceRandom = Random.Range(0, preguntasDelCliente.Count);
        return preguntasDelCliente[indiceRandom];
    }
}