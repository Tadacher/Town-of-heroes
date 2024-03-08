using Metagameplay.Buildings;
using Metagameplay.Ui;
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

    public void Initialize(AbstractMetaGridCell building,
                           DescriptionAreaView descriptionAreaView,
                           MetaBuildingService metaBuildingService)
    {
        _image.sprite = building.Image;
        _title.text = building.Name;
        LinkedPrefab = building;
        _descriptionAreaView = descriptionAreaView;

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
       _metaBuildingService.InstantiateBuilding(LinkedPrefab.GetType());
    }

    public void Deselect()
    {
        _outline.enabled = false;
        _descriptionAreaView.OnBuildButtonPressed -= SpawnBuilding;
    }
}
