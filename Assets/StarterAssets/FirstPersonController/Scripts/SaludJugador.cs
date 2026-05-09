using UnityEngine;
using UnityEngine.SceneManagement;

public class SaludJugador : MonoBehaviour
{
    [SerializeField] private Salud salud;
    [SerializeField] private float tiempoAntesReiniciar = 2f;

    private bool yaMurio = false;

    private void Update()
    {
        if (!yaMurio && salud != null && salud.EstaMuerto())
        {
            yaMurio = true;
            Invoke(nameof(ReiniciarNivel), tiempoAntesReiniciar);
        }
    }

    private void ReiniciarNivel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}