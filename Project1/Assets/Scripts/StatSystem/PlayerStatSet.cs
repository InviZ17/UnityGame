using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerStatSet", menuName = "Stats/PlayerStatSet")]
public class PlayerStatSet : ScriptableObject
{
    public List<CharacterStats.StatValuePair> BaseStats;
}