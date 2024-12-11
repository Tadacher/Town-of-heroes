using Unity.IO.LowLevel.Unsafe;
using static UnityEditor.Progress;

internal class BlockDefenciveAbility : AbstractPassiveDefensiveAbility
{
    private float _block;

    public BlockDefenciveAbility(float block, int priority) : base(priority)
    {
        _block = block;
    }
    public override float ProcessDamage(float damage)
    {
        float realDamage = damage - _block;
        if (realDamage < 0) { realDamage = 0; }
        return realDamage;
    }
}