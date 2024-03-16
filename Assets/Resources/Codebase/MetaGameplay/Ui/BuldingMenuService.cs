using Metagameplay.Buildings;
using Progress;
using UnityEngine;

namespace Metagameplay.Ui
{
    /// <summary>
    /// BuildingMenu and entries logic
    /// </summary>
    public class BuldingMenuService
    {
        private readonly ResourceService _resourceService;
        private string _buildingPrefabPath = "Prefabs/CityBuildings";
        private BuildMenuUiContainer _container;

        public BuldingMenuService(MetaUiContainer metaUiContainer,
                                  MetaBuildingService metaBuildingService,
                                  ResourceService resourceService)
        {
            _container = metaUiContainer.BuildMenuUiContainer;
            AbstractMetaGridCell[] buildings = LoadBuildings();

            foreach (AbstractMetaGridCell building in buildings)
            {
                BuildMenuEntry entry = GameObject.Instantiate(_container.BuildMenuEntryPrefab, _container.BuildingEntriesParent);
                entry.Initialize(building: building,
                                 descriptionAreaView: _container.DescriptionAreaView,
                                 resourceService: resourceService,
                                 metaBuildingService: metaBuildingService);
            }
            _container.BuildingEntriesParent.GetChild(0).GetComponent<BuildMenuEntry>().Select(); //select first entry
            _resourceService = resourceService;
        }

        private AbstractMetaGridCell[] LoadBuildings() => Resources.LoadAll<AbstractMetaGridCell>(_buildingPrefabPath);
    }
}

