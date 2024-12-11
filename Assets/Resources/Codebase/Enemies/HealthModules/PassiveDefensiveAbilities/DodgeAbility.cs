using Services;
using UnityEngine;
using UnityEngine.UIElements;

public class DodgeAbility : AbstractPassiveDefensiveAbility
{
    private const string _missText = "MISS!";
    private float _chance;
    private DamageTextService _damageTextService;
    private Transform _unitTransform;

    public DodgeAbility(float chance, DamageTextService damageTextService, Transform unitTransform)
    {
        _damageTextService = damageTextService;
        _chance = chance;
        _unitTransform = unitTransform;
    }

    public override float ProcessDamage(float damage)
    {
        if (Random.Range(0f, 1f) > _chance)
        {
            _damageTextService.ReturnDamageText(damage, _unitTransform.position);
            return damage;
        }
        else
        {
            _damageTextService.ReturnCustomDamageText(_missText, _unitTransform.position);
            return 0;
        }
    }

    public override void ReInit()
    {
    }
}