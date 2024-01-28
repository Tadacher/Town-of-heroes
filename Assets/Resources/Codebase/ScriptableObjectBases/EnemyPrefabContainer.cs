using UnityEngine;
[CreateAssetMenu(fileName = "EnemyPrefabContainer", menuName = "ScriptableObjects/EnemyPrefabContainer", order = 1)]
public class EnemyPrefabContainer : ScriptableObject
{
    public AbstractEnemy[] Enemies;

    [Header("Goblins")]
    public AbstractEnemy GoblinTrapper;
    public AbstractEnemy Goblin;
}