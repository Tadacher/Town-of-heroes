using BootStrap;
using Codebase.MonobehaviourComponents;
using Codebase.Services.CardGeneration;
using Services;
using Services.GridSystem;
using Services.TowerBuilding;
using UnityEngine;

namespace Infrastructure
{
    public class GameplaySceneInstaller : AbstractMonoInstaller
    {
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private EnemySpawnPosMarker _spawnPosMarker;
        [SerializeField] private EnemyPrefabContainer _enemyPrefabContainer;
        [SerializeField] private SceneBootstrapper _sceneBootstrapper;
        [SerializeField] private TowerCardSpawnMarker _towerCardSpawnMarker;
        public override void InstallBindings()
        {
            //scriptableObjects
            BindScriptableObject(_enemyPrefabContainer);

            //Unity components
            BindUnityComponent(_audioSource);
            
            //Monobeh
            BindMonobehaviour(_spawnPosMarker);
            BindMonobehaviour(_towerCardSpawnMarker);
            //Non-monobeh
            BindService<TowerBuildingService>();
            BindService<TowerInstantiationService>();
            BindService<CardInstantiationService>().NonLazy();
            BindService<DamageTextService>();
            BindService<WaveService>();
            BindService<GridAlignService>();
            //Non monobeh abstract
            BindAbstractClass<AbstractSoundPlayerService, SimpleSoundPlayerService>();

            Container.Inject(_sceneBootstrapper);
        }
    }
}
