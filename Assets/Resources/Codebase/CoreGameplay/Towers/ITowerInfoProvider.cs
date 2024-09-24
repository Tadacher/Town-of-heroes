namespace Core.Towers
{
    public interface ITowerInfoProvider
    {
        public string Name { get; }
        public int Level { get; }
        public float Damage { get; }
        public float Range {  get; }
        public float Delay {  get; }
        
    }
}