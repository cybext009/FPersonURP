using UnityEngine;
using UnityEngine.InputSystem; // Importante para leer el mouse directo

public class PruebaClic : MonoBehaviour
{
    void Update()
    {
        // Nos aseguramos de que haya un ratón conectado y leemos el clic derecho (rightButton)
        if (Mouse.current != null && Mouse.current.rightButton.wasPressedThisFrame)
        {
            Debug.Log("¡ÉXITO! El ratón funciona y Unity detecta el clic derecho.");
        }

        // Prueba extra por si acaso, con el clic izquierdo (leftButton)
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("Clic IZQUIERDO detectado.");
        }
    }
}