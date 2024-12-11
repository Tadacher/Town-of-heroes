using Services;
using System.Collections.Generic;
using UnityEngine;

public class DefaultDamageRecievingModuile : AbstractDamageRecievingModule
{
    public DefaultDamageRecievingModuile(List<AbstractPassiveDefensiveAbility> passiveDefensiveAbilities, Transform unitTransform, DamageTextService damageTextService) : base(unitTransform, damageTextService)
    {
        _passiveDefensiveAbilities = passiveDefensiveAbilities;
    }

    public override void ReInit()
    {
        //reinit all abilities i dunno
    }

    protected override void InitPassiveDefenciveAbilities()
    {
        //init em if needed
    }
}