using UnityEngine;

[CreateAssetMenu(fileName = "TowerStats", menuName = "ScriptableObjects/TowerStats", order = 1)]
public class TowerStats : ScriptableObject
{
    [Header("Base stats")]
    public int AttackDamage;
    public float AttackRange;
    public float AttackDelay;

    [Space(5)]

    [Header("Stats per level")]
    public int AttackDamagePerLevel;
    public float AttackRangePerLevel;
    public float AttackDelayPerLevel;

    public float ExpPerLevel;

    [Space(5)]
    [Header("Audio")]
    public AudioClip AttackClip;
    public AudioClip LevelupClip;

    [Space(5)]
    [Header("Projectile")]
    public ProjectileBehaviour ProjectilePrefab;
    public float ProjectileSpeed;

}

