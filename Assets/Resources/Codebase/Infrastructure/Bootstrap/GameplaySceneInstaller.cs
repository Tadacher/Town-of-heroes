using BootStrap;
using Codebase.MonobehaviourComponents;
using Services;
using Services.CardGeneration;
using Services.GlobalMap;
using Services.GridSystem;
using Services.TowerBuilding;
using Services.Ui;
using UnityEngine;
using WorldCellBuilding.CardImage;
using WorldCells;

namespace Infrastructure
{
    public class GameplaySceneInstaller : AbstractMonoInstaller
    {
        [SerializeField] private SceneBootstrapper _sceneBootstrapper;
        [SerializeField] private AudioSource _audioSource;

        [Header("Markers")]
        [SerializeField] private EnemySpawnPosMarker _spawnPosMarker;
        [SerializeField] private TowerCardSpawnMarker _towerCardSpawnMarker;
        [SerializeField] private MainCameraMarker _mainCameraMarker;

        [Header("SceneUiContainers")]
        [SerializeField] private CardInfoUIContainer _cardInfoUIContainer;
        [SerializeField] private GameplayCanvasContainer _gameplayCanvasContainer;
        [SerializeField] private MapCanvasContainer _mapCanvasContainer;

        [Header("Scriptables")]
        [SerializeField] private EnemyPrefabContainer _enemyPrefabContainer;
        [SerializeField] private WorldCellCardsConfig _worldCellCardsConfig;
        [SerializeField] private EnemyTypeToBiomeSettings _enemyTypeToBiomeSettings;
        [SerializeField] private CardImageDatabase _cardImageDatabase;
        [SerializeField] private TowerImageDatabase _towerImageDatabase;


        public override void InstallBindings()
        {
            //переместить в foreach для T : ScriptqableObject
            //ScriptableObjects
            BindScriptableObject(_enemyPrefabContainer);
            BindScriptableObject(_worldCellCardsConfig);
            BindScriptableObject(_enemyTypeToBiomeSettings);
            BindScriptableObject(_cardImageDatabase);
            BindScriptableObject(_towerImageDatabase);
            _cardImageDatabase.Initialize();
            _towerImageDatabase.Initialize();

            //Unity components
            BindUnityComponent(_audioSource);
            
            //Monobeh 
            BindMonobehaviour(_spawnPosMarker);
            BindMonobehaviour(_cardInfoUIContainer);
            //Markers scripts
            BindMonobehaviour(_towerCardSpawnMarker);
            BindMonobehaviour(_mainCameraMarker);
            BindMonobehaviour(_mapCanvasContainer);

            //Containers
            BindMonobehaviour(_gameplayCanvasContainer);
            
            //Towers
            BindService<TowerBuildingService>();
            BindService<BattleGridService>();
            BindService<TowerInstantiationService>();
            BindService<DamageTextService>();

            //cards
            BindService<CardInstantiationService>().NonLazy();
            BindService<CardGenerationService>();
            BindService<CardDeckService>();
            BindService<WorldCellCardGenerator>();
            BindService<CardDescriptionService>();
            BindService<CardInfoUiService>();

            //worldCells
            BindService<WorldCellInstantiationService>();
            BindService<WorldCellGridService>();
            BindService<WorldCellBuildingService>();
            BindService<WorldCellBalanceService>();

            //enemy waves
            BindService<WaveService>();
            BindService<WaveGenerator>();
            BindService<EnemyInstantiationService>();
            BindService<EnemyTypeService>();

            BindService<CameraPositionService>();
            BindService<GameplayStateMachine>().NonLazy();
            BindService<GameTimeService>();
            BindService<UiService>().NonLazy();
            BindService<UiButtonBinder>().NonLazy();
            //Non monobeh abstract
            BindAbstractClass<AbstractSoundPlayerService, SimpleSoundPlayerService>();

            //Container.Inject(_sceneBootstrapper);

            Debug.Log("SCENE_INSTALLER_DONE");
        }
    }
}
