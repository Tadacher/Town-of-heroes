using Services;
using UnityEngine;

public class DodgeDamageRecievingModule : AbstractDamageRecievingModule
{
    private const string _missText = "MISS!";
    private float _chance;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="chance">Chance from 0 to 1f</param>
    public DodgeDamageRecievingModule(float chance, Transform unitTransform, DamageTextService damageTextService) : base(unitTransform, damageTextService)
    {
        _damageTextService = damageTextService;
        _chance = chance;
    }

    public override float CalculateRecievedDamage(float damage)
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