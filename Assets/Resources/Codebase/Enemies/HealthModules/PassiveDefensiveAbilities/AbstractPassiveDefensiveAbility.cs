public abstract class AbstractPassiveDefensiveAbility
{
    /// <summary>
    /// must be no abilities with same priority except 0
    /// </summary>
    public int Priority;

    protected AbstractPassiveDefensiveAbility(int priority)
    {
        Priority = priority;
    }

    public abstract float ProcessDamage(float damage);
    public abstract void ReInit();
}

