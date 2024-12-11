using Services;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Defines base for enemy damage recieving module
/// </summary>
public abstract class AbstractDamageRecievingModule
{
    protected DamageTextService _damageTextService;
    protected Transform _unitTransform;
    protected List<AbstractPassiveDefensiveAbility> _passiveDefensiveAbilities;
    protected AbstractDamageRecievingModule(Transform unitTransform, DamageTextService damageTextService)
    {
        _unitTransform = unitTransform;
        _damageTextService = damageTextService;
        InitPassiveDefenciveAbilities();
    }

    public abstract void ReInit();
    protected abstract void InitPassiveDefenciveAbilities();

    /// <summary>
    /// calculates damage recieved by enemy.
    /// shuffles throgh sorted ability array and applies effects one by one
    /// </summary>
    /// <param name="damage">flat incoming damage</param>
    /// <returns>calculated damage</returns>
    public float CalculateRecievedDamage(float damage)
    {
        for (int i = 0; i < _passiveDefensiveAbilities.Count; i++)
        {
            damage = _passiveDefensiveAbilities[i].ProcessDamage(damage);
        }
        _damageTextService.ReturnDamageText(damage, _unitTransform.position);
        return damage;
    }

    /// <summary>
    /// Sorts abilities by priority, descending
    /// </summary>
    protected void SortPassiveAbilityOrder(AbstractPassiveDefensiveAbility[] abilityList) => 
        _passiveDefensiveAbilities = abilityList.OrderByDescending(a => a.Priority).ToList();
}

public abstract class AbstractPassiveDefensiveAbility
{
    /// <summary>
    /// must be no abilities with same priority except 0
    /// </summary>
    public int Priority;
    public abstract float ProcessDamage(float damage);
}