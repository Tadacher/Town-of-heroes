using Core.Towers;

public class TowerFactory : AbstractPoolerFactory<AbstractTower>
{
    public override AbstractTower GetObject()
    {
        throw new System.NotImplementedException();
    }

    public override void ReturnToPool(IPoolableObject returnable)
    {
        throw new System.NotImplementedException();
    }

    protected override void ActionOnDestroy(AbstractTower poolable)
    {
        throw new System.NotImplementedException();
    }

    protected override void ActionOnGet(AbstractTower poolable)
    {
        throw new System.NotImplementedException();
    }

    protected override void ActionOnRelease(AbstractTower type)
    {
        throw new System.NotImplementedException();
    }

    protected override AbstractTower CreateNew()
    {
        throw new System.NotImplementedException();
    }
}
