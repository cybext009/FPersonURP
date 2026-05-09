using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Equipo : MonoBehaviour
{
    [SerializeField] EquipoEnum equipo = EquipoEnum.Trampas;
    public EquipoEnum EquipoActual
    {
        get {  return equipo; }
        private set { equipo = value; }
    }
}
