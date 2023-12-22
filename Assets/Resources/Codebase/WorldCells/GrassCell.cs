public class GrassCell : AbstractWorldCell
{
    public override void InsertSelfToGrid()
    {
        base.InsertSelfToGrid();
        _worldCellBalanceService.Count(this);
    }
}