using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

public class Ability : ScriptableObject
{


    public new string name;
    public float cooldownTime;
    public float activeTime;

    public virtual void Activate(GameObject parent) {}
}
