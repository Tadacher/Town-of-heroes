﻿public abstract class AbstractPassiveDefensiveAbility
{
    /// <summary>
    /// must be no abilities with same priority except 0
    /// </summary>
    public int Priority;


    public abstract float ProcessDamage(float damage);
    public abstract void ReInit();
}

