public class SimpleProjectileFactory : AbstractProjectileFactory
{
    public SimpleProjectileFactory(ProjectileBehaviour projectilePrefab) : base(projectilePrefab) => TryInitializePool();
}
