using Services;
using UnityEngine;
/// <summary>
/// Defines base for enemy damage recieving module
/// </summary>
public abstract class AbstractDamageRecievingModule
{
    protected DamageTextService _damageTextService;
    protected Transform _unitTransform;

    protected AbstractDamageRecievingModule(Transform unitTransform, DamageTextService damageTextService)
    {
        _unitTransform = unitTransform;
        _damageTextService = damageTextService;
    }
    public abstract void ReInit();
    /// <summary>
    /// calculates damage recieved by enemy
    /// </summary>
    /// <param name="damage">flat incoming damage</param>
    /// <returns>calculated damage</returns>
    public abstract float CalculateRecievedDamage(float damage);
}
