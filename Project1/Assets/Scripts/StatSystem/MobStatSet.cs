using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New MobStatSet", menuName = "Stats/MobStatSet")]
public class MobStatSet : ScriptableObject
{
    public List<CharacterStats.StatValuePair> BaseStats;
}