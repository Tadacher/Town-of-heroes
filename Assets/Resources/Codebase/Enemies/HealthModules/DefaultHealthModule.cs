using Services;
using UnityEngine;

public class DefaultHealthModule : AbstractDamageRecievingModule
{
    public DefaultHealthModule(Transform unitTransform, DamageTextService damageTextService) : base(unitTransform, damageTextService)
    {
        
    }

    public override int CalculateRecievedDamage(int damage)
    {
        _damageTextService.ReturnDamageText(damage, _unitTransform.position);
        return damage;
    }
}
