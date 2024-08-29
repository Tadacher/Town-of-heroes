using Metagameplay.Buildings;

public abstract class AbstractDialogEventListener
{

}
public class BuildingPlacedDialogEventListener : AbstractDialogEventListener
{
    private IBuildingPlacedEventProvider _buildingPlacedEventProvider;
    private readonly DialogData _dialogData;
    private readonly DialogService _dialogService;
    private bool _isShown;
    public BuildingPlacedDialogEventListener(IBuildingPlacedEventProvider buildingPlacedEventProvider, DialogData dialogData, DialogService dialogService)
    {
        _buildingPlacedEventProvider = buildingPlacedEventProvider;
        _dialogData = dialogData;
        _dialogService = dialogService;
        _buildingPlacedEventProvider.OnMetaGridCellPlaced += OnBuildingPlacedHandler;
    }

    public void OnBuildingPlacedHandler(object building)
    {
        if (building.GetType() == _dialogData.TriggeredType.GetType())
            _dialogService.ShowDialog(_dialogData.Head);
    }
}