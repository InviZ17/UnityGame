using System.Collections.Generic;
using UnityEngine;

public class Stats : AbstractStats
{
    [SerializeField] private StatSet _statSet;

    protected override void InitializeStats()
    {
        if (_statSet != null)
        {
            baseStats = new List<StatValuePair>(_statSet.BaseStats);
        }

        base.InitializeStats();
    }
}