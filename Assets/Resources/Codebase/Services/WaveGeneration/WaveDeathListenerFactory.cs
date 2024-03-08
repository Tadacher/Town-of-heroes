using Services.Factories;
using Zenject;

public class WaveDeathListenerFactory : AbstractPoolerFactory<WaveDeathListener>
{
    public WaveDeathListenerFactory(DiContainer diContainer) : base(diContainer)
    {

    }

    protected override void ActionOnDestroy(WaveDeathListener poolable)
    {
        
    }

    protected override void ActionOnGet(WaveDeathListener poolable)
    {
      
    }

    protected override void ActionOnRelease(WaveDeathListener type)
    {
        
    }

    protected override WaveDeathListener CreateNew() => new WaveDeathListener(this);
}
