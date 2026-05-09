using System.Collections;
using UnityEngine;

public class Arma : MonoBehaviour
{
    public enum TipoDisparo
    {
        Simple,
        Shotgun,
        Rafaga
    }

    [Header("Configuración General")]
    [SerializeField] private TipoDisparo tipoDisparo;

    [SerializeField] private float ataque = 10f;
    [SerializeField] private float tiempoEntreDisparo = 0.5f;
    [SerializeField] private float rango = 100f;
    [SerializeField] private LayerMask layerMask;

    [Header("Referencias")]
    [SerializeField] private Transform cameraPrimeraPersona;
    [SerializeField] private Transform origenProyectil;
    [SerializeField] private TrailRenderer trailPrefab;

    [Header("Shotgun")]
    [SerializeField] private int cantidadPerdigones = 6;
    [SerializeField] private float dispersion = 0.1f;

    [Header("Ráfaga")]
    [SerializeField] private int cantidadRafaga = 3;
    [SerializeField] private float tiempoEntreBalasRafaga = 0.15f;

    private bool puedeDisparar = true;

    public void ProcesarEntrada(bool value)
    {
        if (puedeDisparar && value)
        {
            StartCoroutine(Disparar());
        }
    }

    private IEnumerator Disparar()
    {
        puedeDisparar = false;

        switch (tipoDisparo)
        {
            case TipoDisparo.Simple:
                DisparoSimple();
                break;

            case TipoDisparo.Shotgun:
                yield return StartCoroutine(DisparoShotgun());
                break;

            case TipoDisparo.Rafaga:
                yield return StartCoroutine(DisparoRafaga());
                break;
        }

        yield return new WaitForSecondsRealtime(tiempoEntreDisparo);
        puedeDisparar = true;
    }

    private void DisparoSimple()
    {
        ProcesarRaycast(CalcularDireccion());
    }

    private IEnumerator DisparoShotgun()
    {
        for (int i = 0; i < cantidadPerdigones; i++)
        {
            Vector3 direccion = CalcularDireccionConDispersion();
            ProcesarRaycast(direccion);
        }

        yield return null;
    }

    private IEnumerator DisparoRafaga()
    {
        for (int i = 0; i < cantidadRafaga; i++)
        {
            ProcesarRaycast(CalcularDireccion());
            yield return new WaitForSecondsRealtime(tiempoEntreBalasRafaga);
        }
    }

    private void ProcesarRaycast(Vector3 direccion)
    {
        if (Physics.Raycast(cameraPrimeraPersona.position, direccion, out RaycastHit hit, rango, layerMask))
        {
            Debug.Log("Golpeó: " + hit.transform.name);
            TrailRenderer trail = Instantiate(trailPrefab, origenProyectil.position, Quaternion.identity);
            StartCoroutine(MoverTrail(trail, hit));

            if (hit.transform.TryGetComponent<Salud>(out Salud saludObjetivo))
            {
                saludObjetivo.PerderSalud(ataque);
            }
        }
    }

    private IEnumerator MoverTrail(TrailRenderer trail, RaycastHit hit)
    {
        float t = 0f;

        while (t < 1)
        {
            trail.transform.position = Vector3.Lerp(origenProyectil.position, hit.point, t);
            t += Time.deltaTime / trail.time;
            yield return null;
        }

        trail.transform.position = hit.point;
        Destroy(trail.gameObject, trail.time);
    }

    private Vector3 CalcularDireccion()
    {
        return cameraPrimeraPersona.forward;
    }

    private Vector3 CalcularDireccionConDispersion()
    {
        Vector3 direccion = cameraPrimeraPersona.forward;

        direccion.x += Random.Range(-dispersion, dispersion);
        direccion.y += Random.Range(-dispersion, dispersion);
        direccion.z += Random.Range(-dispersion, dispersion);

        return direccion.normalized;
    }
}