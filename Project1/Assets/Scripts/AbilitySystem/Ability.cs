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

    // consts
    public const int SkillIndexDash = 0;
    public const int SkillIndexFirst = 1;
    public const int SkillIndexSecond = 2;
    public const int SkillIndexThird = 3;
    public const int SkillIndexFourth = 4;
    public const bool SkillStatusUsing = true;
    public const bool SkillStatusNotUsing = false;
}
