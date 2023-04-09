using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractStats : MonoBehaviour
{
    [System.Serializable]
    public struct StatValuePair
    {
        public StatName statName;
        public float value;
    }

    [SerializeField] protected List<StatValuePair> baseStats;

    protected Dictionary<string, float> stats;

    protected virtual void Awake()
    {
        InitializeStats();
    }

    protected virtual void InitializeStats()
    {
        stats = new Dictionary<string, float>();
        foreach (StatValuePair statValuePair in baseStats)
        {
            stats[statValuePair.statName.Name] = statValuePair.value;
        }
    }

    public float GetStatValueByName(string statName)
    {
        if (stats.TryGetValue(statName, out float value))
        {
            return value;
        }
        return 0;
    }

    public void ModifyStatValueByName(string statName, float value)
    {
        if (stats.ContainsKey(statName))
        {
            stats[statName] += value;
        }
    }
}