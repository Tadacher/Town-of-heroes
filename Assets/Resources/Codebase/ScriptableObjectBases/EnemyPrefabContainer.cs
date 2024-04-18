using UnityEngine;
[CreateAssetMenu(fileName = "EnemyPrefabContainer", menuName = "ScriptableObjects/InitializableConfig/EnemyPrefabContainer", order = 1)]
public class EnemyPrefabContainer : ScriptableObject, IInitializableConfig
{
    private const string _enemiesPrefabsPath = "Prefabs/Enemies";
    private AbstractEnemy[] _enemies;
    public AbstractEnemy[] Enemies => _enemies;

    void IInitializableConfig.Initialize()
    {
        _enemies = Resources.LoadAll<AbstractEnemy>(_enemiesPrefabsPath);
    }
}
