using Services;
using UnityEngine;

public class BlockHealthModule : AbstractDamageRecievingModule
{
    private int _block;
    public BlockHealthModule(Transform unitTransform, DamageTextService damageTextService, int block) : base(unitTransform, damageTextService)
    {
        _block = block;
    }

    public override float CalculateRecievedDamage(float damage)
    {
        float realDamage = damage - _block;
        if (realDamage < 0) { realDamage = 0; }
        _damageTextService.ReturnDamageText(realDamage, _unitTransform.position);
        return realDamage;
    }
}
