using UnityEngine;
using UnityEngine.InputSystem;

public class ControlArma : MonoBehaviour
{
    [SerializeField] private Arma armaActual;

    public void AlDisparar(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (armaActual != null)
            {
                armaActual.ProcesarEntrada(true);
            }
        }
    }
}