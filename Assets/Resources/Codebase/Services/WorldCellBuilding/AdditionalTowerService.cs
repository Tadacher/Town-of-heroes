using Services.TowerBuilding;

public class AdditionalTowerService
{
    private IWorldCellPlacedEventProvider _worldCellPlacedEventProvider;
    private IAdditionalTowerCountReviever _additionalTowerCountReviever;

    private int _placedCellCount;
    private int _cellsPerAdditionalTower = 5;

    public AdditionalTowerService(IWorldCellPlacedEventProvider worldCellPlacedEventProvider, IAdditionalTowerCountReviever additionalTowerCountReviever)
    {
        _worldCellPlacedEventProvider = worldCellPlacedEventProvider;
        _additionalTowerCountReviever = additionalTowerCountReviever;

        _worldCellPlacedEventProvider.OnWorldCellPlaced += OnWorldCellPlacedHandler;
    }

    private void OnWorldCellPlacedHandler()
    {
        _placedCellCount++;
        var additionalTowerCount = _placedCellCount / _cellsPerAdditionalTower;
        _additionalTowerCountReviever.AddTowerCount(additionalTowerCount);
    }
}
