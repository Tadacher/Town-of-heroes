using UnityEngine;

public class BubbleShieldAbility : AbstractPassiveDefensiveAbility
{
    private int _bubbleShieldCharge;
    private int _initialBubbleShieldCharge;

    public BubbleShieldAbility(int bubbleShieldCharge)
    {
        _bubbleShieldCharge = bubbleShieldCharge;
    }
    public override float ProcessDamage(float damage)
    {
        if (_bubbleShieldCharge > 0) { damage = 0; }
        _bubbleShieldCharge--;
        Debug.Log($"Shield defend:{damage}, cost shield:{_bubbleShieldCharge}");
        return damage;
    }

    public override void ReInit()
    {
        _bubbleShieldCharge = _initialBubbleShieldCharge;
    }
}
