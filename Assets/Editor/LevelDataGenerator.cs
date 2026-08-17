#if UNITY_EDITOR
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using VentaDeBebidas.Data;
using VentaDeBebidas.Gameplay;

namespace VentaDeBebidas.EditorTools
{
    /// <summary>
    /// Genera automáticamente los assets de Product y los 10 LevelData
    /// descritos en Docs/DisenoDeNiveles.md, para no tener que crearlos
    /// todos a mano en el Inspector. Usalo una sola vez al empezar y
    /// después ajustá valores/sprites libremente en el Inspector.
    /// Menú: VentaDeBebidas > Generar Productos y Niveles de Ejemplo
    /// </summary>
    public static class LevelDataGenerator
    {
        private const string ProductsPath = "Assets/Resources/Products";
        private const string LevelsPath = "Assets/Resources/Levels";

        [MenuItem("VentaDeBebidas/Generar Productos y Niveles de Ejemplo")]
        public static void GenerateAll()
        {
            Directory.CreateDirectory(ProductsPath);
            Directory.CreateDirectory(LevelsPath);

            var products = GenerateProducts();
            GenerateLevels(products);

            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            Debug.Log("VentaDeBebidas: productos y 10 niveles de ejemplo generados en " +
                      $"{ProductsPath} y {LevelsPath}.");
        }

        private static Dictionary<string, Product> GenerateProducts()
        {
            var defs = new (string name, int price)[]
            {
                ("Agua Mineral", 3000),
                ("Gaseosa Cola", 5000),
                ("Gaseosa Naranja", 5000),
                ("Jugo Natural", 6000),
                ("Té Frío", 4500),
                ("Energizante", 8000),
                ("Cerveza", 9000),
                ("Agua Saborizada", 4000),
                ("Café Helado", 7000),
                ("Smoothie", 9500),
            };

            var result = new Dictionary<string, Product>();

            foreach (var (name, price) in defs)
            {
                string assetPath = $"{ProductsPath}/Product_{Sanitize(name)}.asset";
                var product = AssetDatabase.LoadAssetAtPath<Product>(assetPath);
                if (product == null)
                {
                    product = ScriptableObject.CreateInstance<Product>();
                    AssetDatabase.CreateAsset(product, assetPath);
                }
                product.productName = name;
                product.price = price;
                EditorUtility.SetDirty(product);
                result[name] = product;
            }

            return result;
        }

        private static void GenerateLevels(Dictionary<string, Product> p)
        {
            CreateLevel("Level_01", "Primeras Ventas", LevelRole.Cajero, LevelDifficulty.Facil,
                startingMoney: 20000,
                catalog: new[] { "Agua Mineral", "Gaseosa Cola", "Gaseosa Naranja" },
                customers: new List<CustomerEntry>
                {
                    Customer1(p, ("Agua Mineral", 1), pay: 5000),
                    Customer1(p, ("Gaseosa Cola", 1), pay: 5000),
                    Customer1(p, ("Gaseosa Naranja", 1), pay: 10000),
                });

            CreateLevel("Level_02", "Primeras Compras", LevelRole.Comprador, LevelDifficulty.Facil,
                startingMoney: 10000,
                catalog: new[] { "Agua Mineral", "Gaseosa Cola", "Jugo Natural" },
                shoppingList: new List<OrderEntry> { Item(p, "Gaseosa Cola", 1), Item(p, "Agua Mineral", 1) });

            CreateLevel("Level_03", "Variedad en el Mostrador", LevelRole.Cajero, LevelDifficulty.Facil,
                startingMoney: 20000,
                catalog: new[] { "Agua Mineral", "Gaseosa Cola", "Gaseosa Naranja", "Jugo Natural", "Té Frío", "Agua Saborizada" },
                customers: new List<CustomerEntry>
                {
                    Customer1(p, ("Jugo Natural", 1), pay: 10000),
                    Customer1(p, ("Té Frío", 1), pay: 5000),
                    Customer1(p, ("Agua Saborizada", 1), pay: 5000),
                    Customer1(p, ("Gaseosa Cola", 1), pay: 5000),
                    Customer1(p, ("Agua Mineral", 1), pay: 5000),
                });

            CreateLevel("Level_04", "El Vuelto Justo", LevelRole.Comprador, LevelDifficulty.Medio,
                startingMoney: 15000,
                catalog: new[] { "Jugo Natural", "Té Frío", "Agua Saborizada", "Gaseosa Cola" },
                shoppingList: new List<OrderEntry> { Item(p, "Jugo Natural", 1), Item(p, "Té Frío", 1), Item(p, "Agua Saborizada", 1) });

            CreateLevel("Level_05", "Cajero con Vuelto", LevelRole.Cajero, LevelDifficulty.Medio,
                startingMoney: 15000,
                catalog: new[] { "Café Helado", "Jugo Natural", "Gaseosa Cola", "Té Frío" },
                customers: new List<CustomerEntry>
                {
                    Customer1(p, ("Café Helado", 1), pay: 10000),
                    Customer1(p, ("Jugo Natural", 1), pay: 10000),
                    Customer1(p, ("Gaseosa Cola", 1), pay: 10000),
                    Customer1(p, ("Té Frío", 1), pay: 5000),
                    Customer1(p, ("Café Helado", 1), pay: 20000),
                    Customer1(p, ("Jugo Natural", 1), pay: 10000),
                });

            CreateLevel("Level_06", "Lista de Compras", LevelRole.Comprador, LevelDifficulty.Medio,
                startingMoney: 12000,
                catalog: new[] { "Agua Mineral", "Té Frío", "Agua Saborizada", "Gaseosa Naranja" },
                shoppingList: new List<OrderEntry>
                {
                    Item(p, "Agua Mineral", 1), Item(p, "Té Frío", 1),
                    Item(p, "Agua Saborizada", 1), Item(p, "Gaseosa Naranja", 1),
                });

            CreateLevel("Level_07", "Hora Pico", LevelRole.Cajero, LevelDifficulty.Medio,
                startingMoney: 15000,
                catalog: new[] { "Agua Mineral", "Gaseosa Cola", "Gaseosa Naranja", "Jugo Natural", "Té Frío", "Agua Saborizada", "Café Helado", "Energizante" },
                customers: MakeCustomers(p, patience: 20f,
                    ("Agua Mineral", 5000), ("Gaseosa Cola", 5000), ("Energizante", 10000),
                    ("Té Frío", 5000), ("Café Helado", 10000), ("Jugo Natural", 10000),
                    ("Gaseosa Naranja", 5000), ("Agua Saborizada", 5000)));

            CreateLevel("Level_08", "Turno Difícil", LevelRole.Cajero, LevelDifficulty.Dificil,
                startingMoney: 15000,
                catalog: new[] { "Agua Mineral", "Gaseosa Cola", "Gaseosa Naranja", "Jugo Natural", "Té Frío", "Agua Saborizada", "Café Helado", "Energizante", "Cerveza", "Smoothie" },
                globalTime: 180f,
                customers: MakeMultiItemCustomers(p, count: 10));

            CreateLevel("Level_09", "Presupuesto Ajustado + Combos", LevelRole.Comprador, LevelDifficulty.Dificil,
                startingMoney: 18000,
                catalog: new[] { "Agua Mineral", "Gaseosa Cola", "Jugo Natural", "Té Frío", "Café Helado" },
                globalTime: 90f,
                shoppingList: new List<OrderEntry>
                {
                    Item(p, "Agua Mineral", 2), Item(p, "Gaseosa Cola", 1),
                    Item(p, "Jugo Natural", 1), Item(p, "Café Helado", 1),
                });

            CreateLevel("Level_10", "Jornada Completa", LevelRole.Cajero, LevelDifficulty.Dificil,
                startingMoney: 20000,
                catalog: new[] { "Agua Mineral", "Gaseosa Cola", "Gaseosa Naranja", "Jugo Natural", "Té Frío", "Agua Saborizada", "Café Helado", "Energizante", "Cerveza", "Smoothie" },
                globalTime: 240f,
                customers: MakeMultiItemCustomers(p, count: 12, somePatient: true));
        }

        // ---------- Helpers ----------

        private static OrderEntry Item(Dictionary<string, Product> p, string name, int qty) =>
            new OrderEntry { product = p[name], quantity = qty };

        private static CustomerEntry Customer1(Dictionary<string, Product> p, (string name, int qty) item, int pay, float patience = 0f) =>
            new CustomerEntry
            {
                order = new List<OrderEntry> { Item(p, item.name, item.qty) },
                paymentAmount = pay,
                patienceSeconds = patience,
            };

        private static List<CustomerEntry> MakeCustomers(Dictionary<string, Product> p, float patience, params (string name, int pay)[] items)
        {
            var list = new List<CustomerEntry>();
            foreach (var (name, pay) in items)
                list.Add(Customer1(p, (name, 1), pay, patience));
            return list;
        }

        private static List<CustomerEntry> MakeMultiItemCustomers(Dictionary<string, Product> p, int count, bool somePatient = false)
        {
            string[] names = { "Agua Mineral", "Gaseosa Cola", "Gaseosa Naranja", "Jugo Natural", "Té Frío",
                                 "Agua Saborizada", "Café Helado", "Energizante", "Cerveza", "Smoothie" };
            var list = new List<CustomerEntry>();
            for (int i = 0; i < count; i++)
            {
                var first = names[i % names.Length];
                var second = names[(i + 3) % names.Length];
                var entry = new CustomerEntry
                {
                    order = new List<OrderEntry> { Item(p, first, 1), Item(p, second, 1) },
                    paymentAmount = (p[first].price + p[second].price) + 5000,
                    patienceSeconds = somePatient && i % 3 == 0 ? 15f : 0f,
                };
                list.Add(entry);
            }
            return list;
        }

        private static void CreateLevel(
            string levelId, string displayName, LevelRole role, LevelDifficulty difficulty,
            int startingMoney, string[] catalog,
            List<CustomerEntry> customers = null, List<OrderEntry> shoppingList = null,
            float globalTime = 0f)
        {
            string assetPath = $"{LevelsPath}/{levelId}.asset";
            var level = AssetDatabase.LoadAssetAtPath<LevelData>(assetPath);
            if (level == null)
            {
                level = ScriptableObject.CreateInstance<LevelData>();
                AssetDatabase.CreateAsset(level, assetPath);
            }

            level.levelId = levelId;
            level.displayName = displayName;
            level.role = role;
            level.difficulty = difficulty;
            level.startingMoney = startingMoney;
            level.globalTimeLimitSeconds = globalTime;
            level.twoStarScore = 70;
            level.threeStarScore = 100;

            level.availableProducts = new List<Product>();
            foreach (var name in catalog)
            {
                var product = AssetDatabase.LoadAssetAtPath<Product>($"{ProductsPath}/Product_{Sanitize(name)}.asset");
                if (product != null) level.availableProducts.Add(product);
            }

            level.customers = customers ?? new List<CustomerEntry>();
            level.shoppingList = shoppingList ?? new List<OrderEntry>();

            EditorUtility.SetDirty(level);
        }

        private static string Sanitize(string name) => name.Replace(" ", "_");
    }
}
#endif
