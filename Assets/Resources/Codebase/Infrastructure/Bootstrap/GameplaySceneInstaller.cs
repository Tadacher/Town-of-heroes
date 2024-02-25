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

namespace Infrastructure
{
    public class GameplaySceneInstaller : AbstractMonoInstaller
    {
        [SerializeField] private SceneBootstrapper _sceneBootstrapper;
        [SerializeField] private AudioSource _audioSource;

        [Header("Markers")]
        [SerializeField] private MonoBehaviourMarker[] _markers;

        [Header("SceneUiContainers")]
        [SerializeField] private CardInfoUIContainer _cardInfoUIContainer;
        [SerializeField] private GameplayCanvasContainer _gameplayCanvasContainer;
        [SerializeField] private MapCanvasContainer _mapCanvasContainer;

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
            BindService<WaveDeathListenerFactory>();
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
