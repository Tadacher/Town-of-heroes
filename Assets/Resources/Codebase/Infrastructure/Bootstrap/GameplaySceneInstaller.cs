using BootStrap;
using Services;
using Services.CardGeneration;
using Services.GlobalMap;
using Services.GridSystem;
using Services.TowerBuilding;
using Services.Ui;
using Unity.VisualScripting;
using UnityEngine;
using WorldCells;
using static WaveGenerator;

namespace Infrastructure
{
    public class GameplaySceneInstaller : AbstractMonoInstaller
    {
        [SerializeField] private AudioSource _audioSource;

        [Header("Markers")]
        [SerializeField] private MonoBehaviourMarker[] _markers;

        [Header("SceneUiContainers")]
        [SerializeField] private CardInfoUIContainer _cardInfoUIContainer;
        [SerializeField] private GameplayCanvasContainer _gameplayCanvasContainer;
        [SerializeField] private MapCanvasContainer _mapCanvasContainer;
        [SerializeField] private MonsterInfoView _monsterInfoView;
        
        [SerializeField] private TowerInfoView _towerInfoView;
        [SerializeField] private WorldCellInfoView _worldCellInfoView;

        [Header("Scriptables")]
        [SerializeField] private ScriptableObject[] _scriptableObjects;

        public override void InstallBindings()
        {
            //ScriptableObjects
            BindScriptables();

            //Unity components
            BindUnityComponent(_audioSource);

            //Markers scripts
            BindMarkers();
            

            //Containers
            BindMonobehaviour(_cardInfoUIContainer);
            BindMonobehaviour(_mapCanvasContainer);
            BindMonobehaviour(_gameplayCanvasContainer);
            BindMonobehaviour(_monsterInfoView);
            BindMonobehaviour(_towerInfoView);
            BindMonobehaviour(_worldCellInfoView);

            //UI
            BindService<MonsterInfoServiceIngame>();



            //Towers
            BindService<TowerBuildingService>();
            BindService<BattleGridService>();
            BindService<TowerInstantiationService>();
            BindService<DamageTextService>();
            BindService<TowerInfoServiceIngame>();

            //cards
            BindService<CardInstantiationService>().NonLazy();
            BindService<CardGenerationService>().NonLazy();
            BindService<CardDeckService>().NonLazy();
            BindService<WorldCellCardGenerator>();
            BindService<CardDescriptionService>();
            BindService<CardInfoUiService>();
            BindInterfacesAndSelfto<CardCountService>();

            //worldCells
            BindService<WorldCellInstantiationService>();
            BindService<WorldCellGridService>();
            BindService<WorldCellBuildingService>();
            BindService<WorldCellBalanceService>();
            BindService<WorldCellInfoService>();

            //enemy waves
            BindService<WaveService>();
            BindService<WaveDeathListenerFactory>();
            BindService<WaveGenerator>();
            BindService<EnemyInstantiationService>();
            BindService<EnemyTypeService>();
            BindService<EnemyGenerationCostService>();

            BindInterfacesAndSelfto<CoreGameplayService>();

            BindService<CameraPositionService>();
            BindService<GameplayStateMachine>().NonLazy();
            BindService<GameTimeService>();
            BindService<CoreGameplaySceneUiService>().NonLazy();
            BindService<UiButtonBinder>().NonLazy();

            //Non monobeh abstract
            BindAbstractClass<AbstractSoundPlayerService, SimpleSoundPlayerService>();

            //Container.Inject(_sceneBootstrapper);

            Debug.Log("SCENE_INSTALLER_DONE");
        }

        private void BindScriptables()
        {
            foreach (var scriptable in _scriptableObjects)
            {
                Container.Bind(scriptable.GetType()).FromInstance(scriptable);
                IInitializableConfig initializable = scriptable as IInitializableConfig;

                if (initializable != null) 
                    initializable.Initialize();
            }
        }

        private void BindMarkers()
        {
            foreach (var marker in _markers)
                Container.Bind(marker.GetType()).FromInstance(marker);
        }
    }
}
