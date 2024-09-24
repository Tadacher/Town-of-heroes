using Metagameplay.Buildings;
using System;


public abstract class AbstractDialogEventListener : IDisposable
{
    public abstract void Dispose();
}
public class BuildingPlacedDialogEventListener : AbstractDialogEventListener
{
    private IBuildingPlacedEventProvider _buildingPlacedEventProvider;
    private readonly DialogData _dialogData;
    private readonly DialogService _dialogService;
    public BuildingPlacedDialogEventListener(IBuildingPlacedEventProvider buildingPlacedEventProvider, DialogData dialogData, DialogService dialogService)
    {
        _buildingPlacedEventProvider = buildingPlacedEventProvider;
        _dialogData = dialogData;
        _dialogService = dialogService;
        _buildingPlacedEventProvider.OnMetaGridCellPlaced += OnBuildingPlacedHandler;
    }

    public void OnBuildingPlacedHandler(AbstractMetaGridCell building)
    {
        if (building.GetType() == _dialogData.TriggeredType.GetType())
            _dialogService.ShowDialog(_dialogData.Head);
        Dispose();
    }

    public override void Dispose()
    {
        _buildingPlacedEventProvider.OnMetaGridCellPlaced -= OnBuildingPlacedHandler;
    }
}