namespace WorldCells
{
    public interface IWorldCellInfoProvider
    {
        public string Name { get; }
        public CellBiomeTypes[] CellTypes { get; }
        public string Description { get; }
    }
}