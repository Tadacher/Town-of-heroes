using Services;
using UnityEngine;

public class DefaultHealthModule : AbstractHealthModule
{
    public DefaultHealthModule(Transform unitTransform, DamageTextService damageTextService):base(unitTransform, damageTextService)
    {
    }

    public override int RecieveDamage(int damage)
    {
        return damage;
    }
}
