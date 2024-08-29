using Metagameplay.Buildings;
using UnityEngine;

namespace Metagameplay.Ui
{
    public class BuildingMenuEntryFactory
    {
        private readonly BuildMenuUiContainer _container;
        private readonly MetaBuildingService _metaBuildingService;
        private readonly IBuildingContainerProvider _buildingContainerProvider;
        public BuildingMenuEntryFactory(MetaUiContainer metaUiContainer, MetaBuildingService metaBuildingService, IBuildingContainerProvider buildingContainerProvider)
        {
            _container = metaUiContainer.BuildMenuUiContainer;
            _metaBuildingService = metaBuildingService;
            _buildingContainerProvider = buildingContainerProvider;
        }

        public void LoadAllEntries()
        {
            foreach (var pair in _buildingContainerProvider.BuildingContainer)
            {
                AbstractMetaGridCell building = pair.Value;

                if (building.HideInBuildingMenu)
                    continue;
                BuildMenuEntry entry = GameObject.Instantiate(_container.BuildMenuEntryPrefab, _container.BuildingEntriesParent);
                entry.Initialize(building: building,
                                 descriptionAreaView: _container.DescriptionAreaView,
                                 metaBuildingService: _metaBuildingService);
            }
            _container.BuildingEntriesParent.GetChild(0).GetComponent<BuildMenuEntry>().Select(); //select first entry
        }
    }
}