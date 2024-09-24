using Metagameplay.Buildings;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Metagameplay.Ui
{
    public class MetaBuildingPrefabsLoader : IBuildingContainerProvider
    {
        private const string _buildingPrefabPath = "Prefabs/CityBuildings";

        public Dictionary<Type, AbstractMetaGridCell> BuildingContainer => _buildingContainer;
        private readonly Dictionary<Type, AbstractMetaGridCell> _buildingContainer = new();

        public MetaBuildingPrefabsLoader()
        {
            AbstractMetaGridCell[] buildings = LoadBuildings();
            foreach (AbstractMetaGridCell building in buildings)
            {
                _buildingContainer.Add(building.GetType(), building);
            }
        }
        private AbstractMetaGridCell[] LoadBuildings() => Resources.LoadAll<AbstractMetaGridCell>(_buildingPrefabPath);

    }
}