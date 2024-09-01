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
    private MetaCityInfoService _metacityInfoService;
    private bool _selected;
    public void Initialize(AbstractMetaGridCell building,
                           DescriptionAreaView descriptionAreaView,
                           MetaBuildingService metaBuildingService,
                           MetaCityInfoService metaCityInfoService)
    {
        _image.sprite = building.Image;
        _title.text = building.Name;
        LinkedPrefab = building;


        _descriptionAreaView = descriptionAreaView;
        _metaBuildingService = metaBuildingService;
        _metacityInfoService = metaCityInfoService;

        _metacityInfoService.OnAvailableBuilingTypeAdded += OnAvailableBuildingTypeAddedHandler;
    }
    private void OnDestroy()
    {
        if (_selected)
            _descriptionAreaView.OnBuildButtonPressed -= SpawnBuilding;
        Debug.Log("Removed listener " + _title.text);
    }
    

    public void Select()
    {
        if(_selected) 
            return;

        _selected = true;
        _descriptionAreaView.UpdateView(LinkedPrefab.Description, this);
        _descriptionAreaView.OnBuildButtonPressed += SpawnBuilding;
        Debug.Log("Added listener " + _title.text);
        _outline.enabled = true;
    }
    public void Deselect()
    {
        _selected = false;
        _outline.enabled = false;
        _descriptionAreaView.OnBuildButtonPressed -= SpawnBuilding;
        Debug.Log("Removed listener " + _title.text);

    }

    public void SetVisibility()
    {
        if (_metacityInfoService.AvailableBuildingTypes.Contains(LinkedPrefab.GetType()) == false)
            Hide();
        else
            Show();
    }


    private void OnAvailableBuildingTypeAddedHandler(Type type)
    {
        if (type == LinkedPrefab.GetType())
            Show();
    }
    private void SpawnBuilding()
    {
        if (_metaBuildingService.TryInstantiateBuilding(LinkedPrefab))
        {
            _descriptionAreaView.CloseMenu();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
