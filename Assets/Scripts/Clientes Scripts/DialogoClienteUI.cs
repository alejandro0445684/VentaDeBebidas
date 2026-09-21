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
        "Podrías cobrarme por favor",
        "Esto sería todo",
        "Hola, cobrame esto rápido",
        "¿Me cobras esto, por favor?"
    };

    [Header("Componentes de UI")]
    [SerializeField] private GameObject globoTextoObjeto; // Tu objeto GloboUI
    [SerializeField] private TextMeshProUGUI textoDialogo;  // Tu TextoDialogo (TextMeshPro)

    [Header("Animación del Cliente")]
    [SerializeField] private Animator animatorClienteFrontal; // Animator del ClienteFrontalUI

    [Header("Configuración de Tiempos y Animaciones")]
    [SerializeField] private float duracionMensaje = 3.5f;
    [SerializeField] private string nombreEstadoHablar = "Hablar";
    [SerializeField] private string nombreEstadoIdle = "Idle";

    private void OnEnable()
    {
        // Se ejecuta cada vez que el panel del cajero se enciende
        IniciarDialogo();
    }

    public void IniciarDialogo()
    {
        // Si hay frases en la lista, elige una al azar
        if (dialogosPosibles != null && dialogosPosibles.Count > 0)
        {
            int indiceAleatorio = Random.Range(0, dialogosPosibles.Count);
            string fraseElegida = dialogosPosibles[indiceAleatorio];

            StartCoroutine(RutinaHablar(fraseElegida));
        }
    }

    private IEnumerator RutinaHablar(string texto)
    {
        // 1. Asignar el texto seleccionado y mostrar el globo
        if (textoDialogo != null) textoDialogo.text = texto;
        if (globoTextoObjeto != null) globoTextoObjeto.SetActive(true);

        // 2. Activar la animación de hablar
        if (animatorClienteFrontal != null)
        {
            animatorClienteFrontal.Play(nombreEstadoHablar);
        }

        // 3. Esperar el tiempo configurado en pantalla
        yield return new WaitForSeconds(duracionMensaje);

        // 4. Ocultar el globo de texto
        if (globoTextoObjeto != null) globoTextoObjeto.SetActive(false);

        // 5. Regresar a la animación de reposo (Idle)
        if (animatorClienteFrontal != null)
        {
            animatorClienteFrontal.Play(nombreEstadoIdle);
        }
    }
}