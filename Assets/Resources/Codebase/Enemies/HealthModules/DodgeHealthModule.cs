using Services;
using UnityEngine;

public class DodgeHealthModule : AbstractHealthModule
{
    private const string _missText = "MISS!";
    private float _chance;

    /// <summary>
    /// Chance from 0 to 1f
    /// </summary>
    /// <param name="chance"></param>
    public DodgeHealthModule(float chance, Transform unitTransform, DamageTextService damageTextService) : base(unitTransform, damageTextService)
    {
        _damageTextService = damageTextService;
        _chance = chance;
    }

    public override int RecieveDamage(int damage)
    {
        if (Random.Range(0f, 1f) > _chance)
        {
            return damage;
        }
        else
        {
            _damageTextService.ReturnCustomDamageText(_missText, _unitTransform.position);
            return 0;
        }
    }
}