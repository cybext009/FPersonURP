using UnityEngine;
using UnityEngine.AI; // Esta línea es obligatoria para usar el NavMesh

public class EnemyAI : MonoBehaviour
{
    // Esta es la casilla vacía donde arrastraremos a tu jugador
    public Transform target;

    // El "radar" de navegación del enemigo
    private NavMeshAgent agent;

    void Start()
    {
        // Al darle Play, el enemigo busca su propio componente de navegación
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // Cada fotograma del juego, le decimos al enemigo que camine hacia la posición actual del jugador
        if (target != null)
        {
            agent.SetDestination(target.position);
        }
    }
}