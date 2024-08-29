public class GlobalTowerBuffService 
{
    public float RangeBonus => _rangeBonus;
    public float DamageBonus => _damageBonus;
    public float AttackSpeedBonus => _attackSpeedBonus;


    private float _rangeBonus;
    private float _damageBonus;
    private float _attackSpeedBonus;

    public void AddRangeBonus(float value) => _rangeBonus += value;
    public void RemoveRangeBonus(float value) => _rangeBonus -= value;

    public void AddDamageBonus(float value) => _damageBonus += value;
    public void RemoveDamageBonus(float value) => _damageBonus -= value;

    public void AddAttackSpeedBonus(float value) => _attackSpeedBonus += value;
    public void RemoveAttackSpeedBonus(float value) => _attackSpeedBonus -= value;
}
