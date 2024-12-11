using Services;
using UnityEngine;

public class BlockHealthModule : AbstractDamageRecievingModule
{
    private int _block;
    public BlockHealthModule(Transform unitTransform, DamageTextService damageTextService, int block) : base(unitTransform, damageTextService)
    {
        _block = block;
    }

    public override void ReInit()
    {

    }

    protected override void InitPassiveDefenciveAbilities()
    {
        _passiveDefensiveAbilities.Add(new BlockDefenciveAbility(_block));
    }
}
