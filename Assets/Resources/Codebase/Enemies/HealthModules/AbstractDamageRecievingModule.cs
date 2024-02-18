using Services;
using UnityEngine;
public abstract class AbstractDamageRecievingModule
{
    protected DamageTextService _damageTextService;
    protected Transform _unitTransform;

    protected AbstractDamageRecievingModule(Transform unitTransform, DamageTextService damageTextService)
    {
        _unitTransform = unitTransform;
        _damageTextService = damageTextService;
    }

    public abstract int CalculateRecievedDamage(int damage);    
}
