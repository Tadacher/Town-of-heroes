using Services;
using System.Collections.Generic;
using UnityEngine;

public class DefaultDamageRecievingModuile : AbstractDamageRecievingModule
{
    public DefaultDamageRecievingModuile(List<AbstractPassiveDefensiveAbility> passiveDefensiveAbilities, Transform unitTransform, DamageTextService damageTextService) : base(unitTransform, damageTextService, passiveDefensiveAbilities)
    {
        _passiveDefensiveAbilities = passiveDefensiveAbilities;
    }

    protected override void InitPassiveDefenciveAbilities()
    {
        //init em if needed
    }
}