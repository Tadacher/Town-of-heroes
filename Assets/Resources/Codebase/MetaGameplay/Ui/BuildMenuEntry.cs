using Metagameplay.Buildings;
using Metagameplay.Ui;
using Progress;
using System;
using UnityEngine;
using UnityEngine.UI;

public class BuildMenuEntry : MonoBehaviour
{
    public AbstractMetaGridCell LinkedPrefab;

    [SerializeField] private Image _image;
    [SerializeField] private Text _title;
    [SerializeField] private Outline _outline;
    private DescriptionAreaView _descriptionAreaView;
    private MetaBuildingService _metaBuildingService;
    private ResourceService _resourceService;

    public void Initialize(AbstractMetaGridCell building,
                           DescriptionAreaView descriptionAreaView,
                           MetaBuildingService metaBuildingService,
                           ResourceService resourceService)
    {
        _image.sprite = building.Image;
        _title.text = building.Name;
        LinkedPrefab = building;
        _descriptionAreaView = descriptionAreaView;
        _resourceService = resourceService;

        _metaBuildingService = metaBuildingService;
    }
    public void Select()
    {
        _descriptionAreaView.UpdateView(LinkedPrefab.Description, this);
        _descriptionAreaView.OnBuildButtonPressed += SpawnBuilding;
        _outline.enabled = true;
    }

    private void SpawnBuilding()
    {
        if (MetaBuildingDescriptionParams.EnoughToBuy(_resourceService.GetResourceData(), LinkedPrefab.Description.Cost))
        {
            _resourceService.SubtractResources(LinkedPrefab.Description.Cost);
            _metaBuildingService.InstantiateBuilding(LinkedPrefab.GetType());
        }
    }

    public void Deselect()
    {
        _outline.enabled = false;
        _descriptionAreaView.OnBuildButtonPressed -= SpawnBuilding;
    }
}
