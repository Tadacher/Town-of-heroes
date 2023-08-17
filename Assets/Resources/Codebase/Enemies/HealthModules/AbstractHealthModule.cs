using Services;
using UnityEngine;
public abstract class AbstractHealthModule
{
    protected DamageTextService _damageTextService;
    protected Transform _unitTransform;

    protected AbstractHealthModule(Transform unitTransform, DamageTextService damageTextService)
    {
        _unitTransform = unitTransform;
        _damageTextService = damageTextService;
    }

    public abstract int RecieveDamage(int damage);    
}
