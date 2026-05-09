using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Salud : MonoBehaviour
{
    [Header("Configuración de Inmunidad")]
    [SerializeField] private float tiempoInmunidad = 1.5f; // Configurable desde el editor    
    [SerializeField] private float saludMax = 3f;
    [SerializeField] private bool destruirAlMorir = true;
    [SerializeField] private float tiempoEnDestruirse = 0f;
    [SerializeField] private UnityEvent<float> alPerderSalud;
    [SerializeField] private UnityEvent alMorir;

    private float saludActual;
    private Animator animator;
    private bool estaMuerto = false;
    public event Action alActualizarSalud;
    private bool esInmune = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        saludActual = saludMax;
    }

    private void Start()
    {
        alActualizarSalud?.Invoke();
    }

    public bool EstaMuerto()
    {
        return estaMuerto;
    }

    public float ObtenerFraccion()
    {
        return saludActual / saludMax;
    }

    public float ObtenerSalud()
    {
        return saludActual;
    }

    public void AjustarSalud(float salud)
    {
        saludActual = salud;
        alActualizarSalud?.Invoke();
    }

    public void PerderSalud(float saludPerdida)
    {
        //  Varificar que no sea inmune en este momento
        if (!esInmune)
        {
            // encender la inmunidad para bloquear futuros daños
            esInmune = true;

            // programar para que la inmunidad se apague después del tiempo indicado
            Invoke("QuitarInmunidad", tiempoInmunidad);

            // (Aquí va tu código original intacto)
            saludActual = Mathf.Max(saludActual - saludPerdida, 0);
            alPerderSalud?.Invoke(saludPerdida);
            alActualizarSalud?.Invoke();

            if (saludActual == 0)
            {
                Morir();
            }
            else
            {
                //animator.SetTrigger("perderSalud");
            }
        }
    }

    // Este es el método que el Invoke llamará cuando se acabe el tiempo de inmuinidad
    private void QuitarInmunidad()
    {
        esInmune = false;
    }
    private void Morir()
    {
        if (estaMuerto) return;

        alMorir?.Invoke();
        estaMuerto = true;

        // 1. Ocultamos al jugador para que parezca que fue destruido
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();
        if (sprite != null)
        {
            sprite.enabled = false;
        }

        // 2. Apagamos sus físicas para que no siga cayendo o moviéndose
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.simulated = false;
        }

        // 3. Ahora el Invoke funcionará perfectamente porque el objeto sigue vivo (solo está invisible)
        Invoke("VolverAlMenu", tiempoEnDestruirse);

        // ¡Nota que eliminamos el bloque de Destroy(gameObject)!
    }

    private void VolverAlMenu()
    {
        SceneManager.LoadScene(0);
    }


    public void Curar(float cantidadCuracion)
    {
        // Si el jugador ya está muerto, no tiene sentido curarlo
        if (estaMuerto) return;

        // Se suma la salud si se pasa del salud maxima el valor se establece en la salud maxima.       
        saludActual = Mathf.Min(saludActual + cantidadCuracion, saludMax);

        // Se notifica a la barra de la salud de la interfaz  que los valores cambiaron
        alActualizarSalud?.Invoke();
    }
}