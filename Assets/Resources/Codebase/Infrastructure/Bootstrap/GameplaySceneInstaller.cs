using BootStrap;
using Codebase.MonobehaviourComponents;
using Codebase.Services.CardGeneration;
using Services;
using Services.GridSystem;
using Services.TowerBuilding;
using Services.Ui;
using UnityEngine;

namespace Infrastructure
{
    public class GameplaySceneInstaller : AbstractMonoInstaller
    {
        [SerializeField] private SceneBootstrapper _sceneBootstrapper;
        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private EnemySpawnPosMarker _spawnPosMarker;
        [SerializeField] private EnemyPrefabContainer _enemyPrefabContainer;

        [SerializeField] private TowerCardSpawnMarker _towerCardSpawnMarker;
        [SerializeField] private MainCameraMarker _mainCameraMarker;

        [SerializeField] private GameplayCanvasContainer _gameplayCanvasContainer;
        [SerializeField] private MapCanvasContainer _mapCanvasContainer;

        public override void InstallBindings()
        {
            //scriptableObjects
            BindScriptableObject(_enemyPrefabContainer);

            //Unity components
            BindUnityComponent(_audioSource);
            
            //Monobeh
            BindMonobehaviour(_spawnPosMarker);

            //Markers
            BindMonobehaviour(_towerCardSpawnMarker);
            BindMonobehaviour(_mainCameraMarker);
            BindMonobehaviour(_mapCanvasContainer);

            //Containers
            BindMonobehaviour(_gameplayCanvasContainer);

            //Non-monobeh
            BindService<GameTimeService>();
            BindService<TowerBuildingService>();
            BindService<TowerInstantiationService>();
            BindService<CardInstantiationService>().NonLazy();
            BindService<DamageTextService>();
            BindService<WaveService>();
            BindService<GridAlignService>();
            BindService<CameraPositionService>();
            BindService<GameplayStateMachine>().NonLazy();
            BindService<UiService>().NonLazy();
            BindService<UiButtonBinder>().NonLazy();
            //Non monobeh abstract
            BindAbstractClass<AbstractSoundPlayerService, SimpleSoundPlayerService>();

            //Container.Inject(_sceneBootstrapper);

            Debug.Log("Scene installer is done");
        }
    }
}
