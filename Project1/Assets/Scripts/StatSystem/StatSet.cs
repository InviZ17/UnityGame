using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New StatSet", menuName = "Stats/StatSet")]
public class StatSet : ScriptableObject
{
    public List<AbstractStats.StatValuePair> BaseStats;
}