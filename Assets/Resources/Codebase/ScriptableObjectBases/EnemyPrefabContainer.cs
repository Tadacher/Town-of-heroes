using UnityEngine;
[CreateAssetMenu(fileName = "EnemyPrefabContainer", menuName = "ScriptableObjects/EnemyPrefabContainer", order = 1)]
public class EnemyPrefabContainer : ScriptableObject, IInitializableConfig
{
    private AbstractEnemy[] _enemies;
    public AbstractEnemy[] Enemies => _enemies;

    public void Initialize()
    {
        _enemies = Resources.LoadAll<AbstractEnemy>("Prefabs/Enemies");
    }
}