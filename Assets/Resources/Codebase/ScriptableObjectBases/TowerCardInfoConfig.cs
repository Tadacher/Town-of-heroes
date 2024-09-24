using System;
using UnityEngine;
/// <summary>
/// Contains info for tower card ui, such as description, title, damage, etc
/// File name must be same as towerstats file name
/// </summary>
[CreateAssetMenu(fileName = "TowerCardInfoConfig", menuName = "ScriptableObjects/TowerCardInfoConfig", order = 1)]
public class TowerCardInfoConfig : ScriptableObject
{
    public string Header;
    public string Decription;
    public string Damage { get => TowerStats.AttackDamage.ToString(); }
    public string DamagePerLevel { get => TowerStats.AttackDamagePerLevel.ToString();}
    public string Range { get => TowerStats.AttackRange.ToString();}
    public string RangePerLevel { get => TowerStats.AttackRangePerLevel.ToString(); }
    public string Interval { get => TowerStats.AttackDelay.ToString();}
    public string IntervalPerLevel { get => TowerStats.AttackDelayPerLevel.ToString(); }
    /// <summary>
    /// insert tower to automatically gain its stats to description
    /// </summary>
    public TowerStats TowerStats;

    
}
