public class ArrowProjectileFactory : AbstractProjectileFactory
{
    public ArrowProjectileFactory(ProjectileBehaviour projectilePrefab) : base(projectilePrefab) => TryInitializePool();
}
