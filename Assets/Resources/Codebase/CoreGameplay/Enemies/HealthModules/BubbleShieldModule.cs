using Services;
using UnityEngine;

public class BubbleShieldModule : AbstractDamageRecievingModule
{
    private int _bubbleShieldCharge;
    private int _initialBubbleShieldCharge;
    public BubbleShieldModule(Transform unitTransform, DamageTextService damageTextService, int bubbleShieldCharge) : base(unitTransform, damageTextService)
    {
        _bubbleShieldCharge = bubbleShieldCharge;
        _initialBubbleShieldCharge = bubbleShieldCharge;
    }
        
    public override float CalculateRecievedDamage(float damage)
    {
        if (_bubbleShieldCharge > 0) { damage = 0; }
        _damageTextService.ReturnDamageText(damage, _unitTransform.position);
        _bubbleShieldCharge--;
        return damage;
    }

    public override void ReInit()
    {
        _bubbleShieldCharge = _initialBubbleShieldCharge;
    }
}
