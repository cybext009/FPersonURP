using System;
using UnityEngine;
using UnityEngine.Events;

public class Salud : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private float tiempoInmunidad = 1.5f;
    [SerializeField] private float saludMax = 3f;
    [SerializeField] private bool destruirAlMorir = true;
    [SerializeField] private float tiempoEnDestruirse = 0f;

    [Header("Eventos")]
    [SerializeField] private UnityEvent<float> alPerderSalud;
    [SerializeField] private UnityEvent alMorir;

    private float saludActual;
    private bool estaMuerto = false;
    private bool esInmune = false;

    public event Action alActualizarSalud;

    private void Awake()
    {
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
        saludActual = Mathf.Clamp(salud, 0, saludMax);
        alActualizarSalud?.Invoke();
    }

    public void PerderSalud(float saludPerdida)
    {
        if (estaMuerto || esInmune)
            return;

        esInmune = true;
        Invoke(nameof(QuitarInmunidad), tiempoInmunidad);

        saludActual = Mathf.Max(saludActual - saludPerdida, 0);

        alPerderSalud?.Invoke(saludPerdida);
        alActualizarSalud?.Invoke();

        if (saludActual == 0)
        {
            Morir();
        }
    }

    public void Curar(float cantidadCuracion)
    {
        if (estaMuerto)
            return;

        saludActual = Mathf.Min(saludActual + cantidadCuracion, saludMax);
        alActualizarSalud?.Invoke();
    }

    private void QuitarInmunidad()
    {
        esInmune = false;
    }

    private void Morir()
    {
        if (estaMuerto)
            return;

        estaMuerto = true;

        alMorir?.Invoke();

        if (destruirAlMorir)
        {
            Destroy(gameObject, tiempoEnDestruirse);
        }
    }
}