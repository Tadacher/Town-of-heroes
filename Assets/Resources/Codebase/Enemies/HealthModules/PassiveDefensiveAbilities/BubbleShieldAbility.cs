using System;
using UnityEngine;
using Services;

public class BubbleShieldAbility : AbstractPassiveDefensiveAbility
{
    private int _bubbleShieldCharge;
    private int _initialBubbleShieldCharge;
    protected DamageTextService _damageTextService;
    protected Transform _unitTransform;

    public BubbleShieldAbility(int bubbleShieldCharge, DamageTextService damageTextService, Transform unitTransform int priority) : base(priority)
    {
        _bubbleShieldCharge = bubbleShieldCharge;
        _damageTextService = damageTextService;
        _unitTransform = unitTransform;
    }
    public override float ProcessDamage(float damage)
    {
        if (_bubbleShieldCharge > 0) { damage = 0; }
        _damageTextService.ReturnDamageText(damage, _unitTransform.position);
        _bubbleShieldCharge--;
        Debug.Log($"Shield defend:{damage}, cost shield:{_bubbleShieldCharge}");
        return damage;
    }

    public override void ReInit()
    {
        _bubbleShieldCharge = _initialBubbleShieldCharge;
    }
}
