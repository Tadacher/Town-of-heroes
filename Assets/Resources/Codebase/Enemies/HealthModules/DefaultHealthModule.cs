using Services;
using System.Collections.Generic;
using UnityEngine;

public class DefaultHealthModule : AbstractDamageRecievingModule
{

    public DefaultHealthModule(Transform unitTransform, DamageTextService damageTextService, List<AbstractPassiveDefensiveAbility> passiveDefensiveAbilities) : base(unitTransform,
                                                                                                                                                                     damageTextService,
                                                                                                                                                                     passiveDefensiveAbilities)
    {
        
    }

    protected override void InitPassiveDefenciveAbilities()
    {

    }
}
