using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorProductosCliente : MonoBehaviour
{
    [Header("Prefabs y Puntos de Spawn")]
    [SerializeField] private GameObject prefabProductoBase; // Un Prefab con el script ProductoEnMesa
    [SerializeField] private Transform[] puntosSpawnMesa;  // Lugares en la mesa donde aparecerán

    [Header("Lista de Productos que Trae el Cliente Actual")]
    [SerializeField] private List<ProductoData> productosDelCliente = new List<ProductoData>();

    [Header("Configuración")]
    [SerializeField] private float tiempoEntreProductos = 0.3f;

    // Llama a este método cuando el cliente llega a la mesa
    public void ColocarProductosEnMesa(List<ProductoData> productos)
    {
        productosDelCliente = productos;
        StartCoroutine(RutinaSoltarProductos());
    }

    private IEnumerator RutinaSoltarProductos()
    {
        for (int i = 0; i < productosDelCliente.Count; i++)
        {
            // Determinar posición de spawn (si hay más productos que puntos, usa el último punto con un desfase)
            Vector3 posicionSpawn = (i < puntosSpawnMesa.Length) 
                ? puntosSpawnMesa[i].position 
                : puntosSpawnMesa[puntosSpawnMesa.Length - 1].position + new Vector3((i - puntosSpawnMesa.Length + 1) * 0.5f, 0, 0);

            // Crear el objeto del producto en la mesa
            // LÍNEA NUEVA (crea el clon DENTRO del punto de spawn directamente):
            GameObject nuevoObjeto = Instantiate(prefabProductoBase, puntosSpawnMesa[i]);

            // Asignarle los datos del producto actual
            ProductoEnMesa productoScript = nuevoObjeto.GetComponent<ProductoEnMesa>();
            if (productoScript != null)
            {
                productoScript.InicializarProducto(productosDelCliente[i]);
            }

            yield return new WaitForSeconds(tiempoEntreProductos);
        }
    }
   private void Start()
{
    Debug.Log("Iniciando Generador. Cantidad de productos en lista: " + productosDelCliente.Count);

    if (productosDelCliente != null && productosDelCliente.Count > 0)
    {
        ColocarProductosEnMesa(productosDelCliente);
    }
    else
    {
        Debug.LogError("¡La lista de productos del cliente está VACÍA en el Inspector!");
    }
}
}