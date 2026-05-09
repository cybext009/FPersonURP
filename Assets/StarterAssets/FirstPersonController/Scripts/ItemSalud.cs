using UnityEngine;

public class ItemSalud : MonoBehaviour
{
    [Header("Configuración del Ítem")]
    [SerializeField] private float cantidadCuracion = 1f; 
    [SerializeField] private AudioClip sonidoCuracion;

    private AudioSource audioSource;
    private SpriteRenderer spriteRenderer;
    private Collider2D colisionador;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        colisionador = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Buscamos el script de Salud 
        Salud saludJugador = collision.GetComponent<Salud>();

        if (saludJugador != null)
        {
            saludJugador.Curar(cantidadCuracion);
            RecolectarYReproducirSonido();
        }
    }

    private void RecolectarYReproducirSonido()
    {
        if (sonidoCuracion != null && audioSource != null)
        {
            audioSource.PlayOneShot(sonidoCuracion);
        }

        if (spriteRenderer != null) spriteRenderer.enabled = false;
        if (colisionador != null) colisionador.enabled = false;

        float duracionSonido = sonidoCuracion != null ? sonidoCuracion.length : 0f;
        Destroy(gameObject, duracionSonido);
    }
}