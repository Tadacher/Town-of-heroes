using Metagameplay.Buildings;
using UnityEngine;

namespace Metagameplay.Ui
{
    /// <summary>
    /// BuildingMenu and entries logic
    /// </summary>
    public class BuldingMenuService
    {
        private string _buildingPrefabPath = "Prefabs/CityBuildings";
        private BuildMenuUiContainer _container;

        public BuldingMenuService(
            MetaUiContainer metaUiContainer,
            MetaBuildingService metaBuildingService)
        {
            _container = metaUiContainer.BuildMenuUiContainer;
            AbstractMetaGridCell[] buildings = LoadBuildings();

            foreach (AbstractMetaGridCell building in buildings)
            {
                BuildMenuEntry entry = GameObject.Instantiate(_container.BuildMenuEntryPrefab, _container.BuildingEntriesParent);
                entry.Initialize(building,
                                 _container.DescriptionAreaView,
                                 metaBuildingService);
            }
            _container.BuildingEntriesParent.GetChild(0).GetComponent<BuildMenuEntry>().Select(); //select first entry
        }

        private AbstractMetaGridCell[] LoadBuildings() => Resources.LoadAll<AbstractMetaGridCell>(_buildingPrefabPath);
    }
}

