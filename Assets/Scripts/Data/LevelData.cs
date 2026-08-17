using System.Collections.Generic;
using UnityEngine;
using VentaDeBebidas.Gameplay;

namespace VentaDeBebidas.Data
{
    public enum LevelRole { Cajero, Comprador }
    public enum LevelDifficulty { Facil, Medio, Dificil }

    /// <summary>
    /// Un pedido: qué productos y en qué cantidad pide un cliente
    /// (rol Cajero) o qué hay que comprar (rol Comprador).
    /// </summary>
    [System.Serializable]
    public class OrderEntry
    {
        public Product product;
        [Min(1)] public int quantity = 1;
    }

    /// <summary>
    /// Un cliente dentro de un nivel de Cajero: su pedido, cuánto paga
    /// y cuánto tiempo de paciencia tiene (0 = sin límite).
    /// </summary>
    [System.Serializable]
    public class CustomerEntry
    {
        public List<OrderEntry> order = new List<OrderEntry>();
        [Min(0)] public int paymentAmount = 0;   // con cuánto dinero paga el cliente
        [Min(0)] public float patienceSeconds = 0f; // 0 = sin límite de tiempo
    }

    /// <summary>
    /// Define un nivel completo. Crear assets:
    /// Assets > Create > VentaDeBebidas > Level Data
    /// Nombrar los assets Level_01 ... Level_10 (LevelDatabase los ordena así).
    /// </summary>
    [CreateAssetMenu(fileName = "Level_", menuName = "VentaDeBebidas/Level Data")]
    public class LevelData : ScriptableObject
    {
        [Header("Identidad")]
        public string levelId = "Level_01";
        public string displayName = "Primeras Ventas";
        public LevelRole role = LevelRole.Cajero;
        public LevelDifficulty difficulty = LevelDifficulty.Facil;

        [Header("Economía")]
        [Tooltip("Caja inicial (rol Cajero) o presupuesto (rol Comprador).")]
        public int startingMoney = 20000;

        [Header("Catálogo disponible en este nivel")]
        public List<Product> availableProducts = new List<Product>();

        [Header("Rol Cajero: lista de clientes a atender en orden")]
        public List<CustomerEntry> customers = new List<CustomerEntry>();

        [Header("Rol Comprador: lista de compra a completar")]
        public List<OrderEntry> shoppingList = new List<OrderEntry>();

        [Header("Reglas del nivel")]
        [Tooltip("0 = sin límite de tiempo global para todo el nivel.")]
        public float globalTimeLimitSeconds = 0f;
        [Tooltip("Cuántos errores de cobro/vuelto o de presupuesto se toleran.")]
        public int maxAllowedErrors = 0;

        [Header("Estrellas")]
        [Tooltip("Puntaje mínimo (0-100) para 2 y 3 estrellas.")]
        public int twoStarScore = 70;
        public int threeStarScore = 100;
    }
}
