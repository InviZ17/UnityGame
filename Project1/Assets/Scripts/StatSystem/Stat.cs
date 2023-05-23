using UnityEngine;

[CreateAssetMenu(fileName = "New Stat", menuName = "Stats/Stat")]
public class Stat : ScriptableObject
{
    public string statName;
    public float baseValue;
}
