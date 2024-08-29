using Metagameplay.Buildings;
using System;
using System.Collections.Generic;

namespace Metagameplay.Ui
{
    public interface IBuildingContainerProvider
    {
        Dictionary<Type, AbstractMetaGridCell> BuildingContainer { get; }
    }
}