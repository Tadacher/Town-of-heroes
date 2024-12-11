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

    protected AbstractDamageRecievingModule(Transform unitTransform, DamageTextService damageTextService, List<AbstractPassiveDefensiveAbility> passiveDefensiveAbilities)
    {
        _unitTransform = unitTransform;
        _damageTextService = damageTextService;
        _passiveDefensiveAbilities = passiveDefensiveAbilities;
        InitPassiveDefenciveAbilities();
    }

    public virtual void ReInit()
    {
        if (_passiveDefensiveAbilities == null)
            return;
        foreach (AbstractPassiveDefensiveAbility ability in _passiveDefensiveAbilities)
        {
            ability.ReInit();
        }
    }
    protected abstract void InitPassiveDefenciveAbilities();

    /// <summary>
    /// calculates damage recieved by enemy.
    /// shuffles throgh sorted ability array and applies effects one by one
    /// </summary>
    /// <param name="damage">flat incoming damage</param>
    /// <returns>calculated damage</returns>
    public float CalculateRecievedDamage(float damage)
    {
        if (_passiveDefensiveAbilities == null)
            return damage;

        foreach (AbstractPassiveDefensiveAbility ability in _passiveDefensiveAbilities)
        {
            damage = ability.ProcessDamage(damage);
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
