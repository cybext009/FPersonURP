using UnityEngine;
using UnityEngine.InputSystem; // Esto es vital

public class ControlArma : MonoBehaviour
{
    public Arma armaActual;

    // Fíjate bien en lo que está dentro de los paréntesis
    public void AlDisparar(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            Debug.Log("1. Botón presionado. Llamando al arma...");

            if (armaActual != null)
            {
                armaActual.ProcesarEntrada(true);
            }
        }
    }
}