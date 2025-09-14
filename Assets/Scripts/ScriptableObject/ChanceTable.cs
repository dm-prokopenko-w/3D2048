using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewChanceTable", menuName = "Chances/Chance Table")]
public class ChanceTable : ScriptableObject
{
    [SerializeField] private List<ChanceEntry> entries = new List<ChanceEntry>();


    public int GetTotalChance()
    {
        int total = 0;
        foreach (var e in entries) total += e.chance;
        return total;
    }

    public List<ChanceEntry> Entries => entries;
}

[System.Serializable]
public class ChanceEntry
{
    public int number;
    public ConfigCube configCube;
    [Range(0, 100)] public int chance;
}