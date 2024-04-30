using Metagameplay.Buildings;
using Metagameplay.Ui;
using Progress;
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
    private bool _selected;
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
        if(_selected) 
            return;

        _selected = true;
        _descriptionAreaView.UpdateView(LinkedPrefab.Description, this);
        _descriptionAreaView.OnBuildButtonPressed += SpawnBuilding;
        Debug.Log("Added listener " + _title.text);
        _outline.enabled = true;
    }

    private void SpawnBuilding()
    {
        if (_resourceService.GetResourceData() >= LinkedPrefab.Description.Cost)
        {
            _resourceService.SubtractResources(LinkedPrefab.Description.Cost);
            _metaBuildingService.InstantiateBuilding(LinkedPrefab.GetType());
            _descriptionAreaView.CloseMenu();
        }
    }
    
    public void Deselect()
    {
        _selected = false;
        _outline.enabled = false;
        _descriptionAreaView.OnBuildButtonPressed -= SpawnBuilding;
        Debug.Log("Removed listener " + _title.text);

    }
    private void OnDestroy()
    {
        if(_selected)
            _descriptionAreaView.OnBuildButtonPressed -= SpawnBuilding;
        Debug.Log("Removed listener " + _title.text);
    }
}
