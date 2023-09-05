using UnityEngine;

[CreateAssetMenu(fileName = "EnemyStats", menuName = "ScriptableObjects/EnemyStats", order = 1)]
public class EnemyStats : ScriptableObject
{
    [Header("Base stats")]
    public int HitPoints;
    public int Damage;
    public int ExpPerKill;

    public float Speed;

    public string Name;
    [Space(5)]

    [Header("Stats per level")]
    public int HitPointsPerLevel;
    public int DamagePerLevel;
    public int ExpPerKillPerLevel;

    public float SpeedPerLevel;

    [Space(5)]
    [Header("Audio")]
    public AudioClip DeathSound;

}

