using UnityEngine;

public class Patrullar : MonoBehaviour
{
    [SerializeField] private float velocidadMovimiento = 2f;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima = 0.2f;
    [SerializeField] private GameObject botonPerspectiva;

    private int indicePuntoActual = 0;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void OnEnable()
    {
        // Al activarse el objeto o panel, siempre empieza desde el punto 0
        indicePuntoActual = 0;
        Girar();
    }

    private void Update()
    {
        if (puntosMovimiento != null && puntosMovimiento.Length > 0 && puntosMovimiento[indicePuntoActual] != null)
    {
        Debug.DrawLine(transform.position, puntosMovimiento[indicePuntoActual].position, Color.red);
        
        Debug.Log($"[DATOS] Cliente está en: {transform.position} | El Punto [{indicePuntoActual}] ({puntosMovimiento[indicePuntoActual].name}) está en: {puntosMovimiento[indicePuntoActual].position}");
    }

        if (puntosMovimiento == null || puntosMovimiento.Length == 0) return;
        if (puntosMovimiento[indicePuntoActual] == null) return;

        // Movimiento directo hacia el punto actual
        transform.position = Vector3.MoveTowards(
            transform.position, 
            puntosMovimiento[indicePuntoActual].position, 
            velocidadMovimiento * Time.deltaTime
        );

        // Comprueba si llegó al punto
        if (Vector3.Distance(transform.position, puntosMovimiento[indicePuntoActual].position) < distanciaMinima)
        {
            if (indicePuntoActual < puntosMovimiento.Length - 1)
            {
                indicePuntoActual++;
                Girar();
            }
            else
            {
                // Llegó al último punto: activa el botón y detiene el patrullaje
                if (botonPerspectiva != null) 
                {
                    botonPerspectiva.SetActive(true);
                }
                this.enabled = false;
            }
        }
    }

    private void Girar()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        if (spriteRenderer != null && puntosMovimiento != null && indicePuntoActual < puntosMovimiento.Length && puntosMovimiento[indicePuntoActual] != null)
        {
            // Apunta la mirada según la dirección del siguiente punto
            spriteRenderer.flipX = transform.position.x < puntosMovimiento[indicePuntoActual].position.x;
        }
    }
}