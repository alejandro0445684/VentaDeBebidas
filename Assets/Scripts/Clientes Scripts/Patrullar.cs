using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullar : MonoBehaviour
{
[SerializeField] private float velocidadMovimiento;

[SerializeField] private Transform[] puntosMovimiento;

[SerializeField] private float distanciaMinima;

[SerializeField] private GameObject botonPerspectiva;

private int numeroAleatorio;

private SpriteRenderer spriteRenderer;

private void Start()
{
    if (puntosMovimiento == null || puntosMovimiento.Length == 0)return;
    numeroAleatorio = Random.Range(0, puntosMovimiento.Length);
    spriteRenderer = GetComponent<SpriteRenderer>();
    Girar();
}

private void Update()
{
    if (puntosMovimiento == null || puntosMovimiento.Length == 0)return;
    if(puntosMovimiento[numeroAleatorio] == null) return;
    
    transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[numeroAleatorio].position, velocidadMovimiento * Time.deltaTime);

    if (Vector2.Distance(transform.position, puntosMovimiento[numeroAleatorio].position) < distanciaMinima)
    {
        if(numeroAleatorio < puntosMovimiento.Length - 1)
            {
                numeroAleatorio++;
            }
            else
            {
                if (botonPerspectiva != null) botonPerspectiva.SetActive (true);
                this.enabled = false;
            }
    }

}

private void Girar()
    {
        if(transform.position.x < puntosMovimiento[numeroAleatorio].position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
    
}
