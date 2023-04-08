using System.Collections.Generic;
using UnityEngine;

public class MobStats : CharacterStats
{
    [SerializeField] private MobStatSet mobStatSet;

    protected override void InitializeStats()
    {
        if (mobStatSet != null)
        {
            baseStats = new List<StatValuePair>(mobStatSet.BaseStats);
        }

        base.InitializeStats();
    }
}