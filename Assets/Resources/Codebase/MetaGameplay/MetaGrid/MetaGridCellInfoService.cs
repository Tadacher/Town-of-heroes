using Metagameplay.Buildings;
/// <summary>
/// shows info about selected meta grid cells
/// </summary>
public class MetaGridCellInfoService
{
    private MetaGridCellInfoView _cellInfoView;
    private ISelectableElement _lastProviderAsSelectable;
    private IMetaGridCellInfoProvider _lastProvider;
    public MetaGridCellInfoService(MetaGridCellInfoView cellInfoView)
    {
        _cellInfoView = cellInfoView;
    }

    public void ShowInfo(IMetaGridCellInfoProvider abstractMetaGridCell, ISelectableElement selectableElement)
    {
        var infoContainer = abstractMetaGridCell.Description;

        _cellInfoView.SetDescriptionText(infoContainer.Description);
        _cellInfoView.SetHeaderText(infoContainer.Name);

        if (abstractMetaGridCell != _lastProvider)
        {
            _lastProviderAsSelectable?.DeSelect();
        }
        _lastProviderAsSelectable = selectableElement;
    }
}
