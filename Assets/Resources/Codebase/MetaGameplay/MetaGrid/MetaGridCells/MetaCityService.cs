using Metagameplay.Buildings;
using System.Collections.Generic;

public class MetaCityService
{
    private HashSet<AbstractMetaGridCell> _metaCells = new();

    public void ApplyEffects()
    {
        foreach (var cell in _metaCells) { cell.ApplyEffect(); }
    }
    public void AddCell (AbstractMetaGridCell cell)
    {
        _metaCells.Add(cell);
    }
}

