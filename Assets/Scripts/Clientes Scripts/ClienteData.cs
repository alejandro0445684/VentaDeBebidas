using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NuevoClienteData", menuName = "Sistema Juego/Cliente Data")]
public class ClienteData : ScriptableObject
{
    [Header("Identificación")]
    public string idCliente;
    public string nombreCliente;

    [Header("Visuales - Vista Mapa (Patrulla)")]
    public Sprite spriteMapa;
    public RuntimeAnimatorController animatorControllerMapa;

    [Header("Visuales - Vista Cajero (Frontal UI)")]
    public Sprite spriteCajeroFrontal;
    public RuntimeAnimatorController animatorControllerCajero;

    [Header("Diálogos del Cajero")]
    [TextArea(2, 4)]
    public List<string> dialogosPosibles = new List<string>()
    {
        "Podrías cobrarme por favor",
        "Esto sería todo por hoy",
        "Hola, cobrame esto rápido"
    };
}