using UnityEngine;

[CreateAssetMenu(fileName = "New StatName", menuName = "Stats/StatName")]
public class StatName : ScriptableObject
{
    [SerializeField] private string statName;
    public string Name => statName;
}