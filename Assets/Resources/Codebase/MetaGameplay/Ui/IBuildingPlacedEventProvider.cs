using Metagameplay.Buildings;
using System;

public interface IBuildingPlacedEventProvider
{
    event Action<AbstractMetaGridCell> OnMetaGridCellPlaced;
}