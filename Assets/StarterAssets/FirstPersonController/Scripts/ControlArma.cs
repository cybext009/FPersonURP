using System.Collections;
using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControlArma : MonoBehaviour
{
    [SerializeField] private Arma arma;

    public void AlDisparar(InputAction.CallbackContext value)
    {
        arma.ProcesarEntrada(value.action.triggered);
    }
}
