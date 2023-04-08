using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    [SerializeField] private PlayerStatSet playerStatSet;

    protected override void InitializeStats()
    {
        if (playerStatSet != null)
        {
            baseStats = new List<StatValuePair>(playerStatSet.BaseStats);
        }

        base.InitializeStats();
    }
}