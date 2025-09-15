using System.Linq;
using UnityEngine;

public class CubePo2Generator
{
    private ChanceTable _chanceTable;
    
    public CubePo2Generator(ChanceTable chanceTable)
    {
        _chanceTable = chanceTable;
    }
    
    public ChanceEntry GetRandomNumber()
    {
        int total = 0;
        foreach (var e in _chanceTable.Entries) total += e.chance;

        float roll = Random.value * total;
        int sum = 0;

        foreach (var e in _chanceTable.Entries)
        {
            sum += e.chance;
            if (roll < sum)
                return e;
        }

        return _chanceTable.Entries.Last();
    }

    public ChanceEntry GetNextChance(int number)
    {
        for (int i = 0; i < _chanceTable.Entries.Count; i++)
        {
            if (_chanceTable.Entries[i].number == number)
            {
                if (i >= _chanceTable.Entries.Count - 1)
                    return null;

                return _chanceTable.Entries[i + 1];
            }
        }

        return null; 
    }
}
