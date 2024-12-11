using Progress;
using Services.CardGeneration;
using Services.Input;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : AbstractMonoInstaller
    {
        [SerializeField] private CoroutineProcessorService _coroutineRunnerPrefab;
        [SerializeField] private InputReciever _inputRecieverPrefab;
        private CoroutineProcessorService _coroutineRunner;
        private InputReciever _inputReciever;
        public override void InstallBindings()
        {
            BindInterfacesAndSelfto<DesctopInput, AbstractInputService>();
            
            InitializeCoroutineRunner();
            InitializeInputListener();

            BindInterfaceFromInstance<ICoroutineRunner, CoroutineProcessorService>(_coroutineRunner);


            BindService<SceneLoaderService>();
            BindService<GameStateFactory>();

            //saveload
            BindService<MetaCitySaveLoader>();
            BindService<CardDeckSaveLoader>();
            BindService<ResourcesSaveLoader>();
            //

            BindService<ResourceService>().NonLazy();


            BindService<GameStateMachine>().NonLazy();
            Debug.Log("BOOTSTRAP INSTALLER DONE");
        }

        private void InitializeInputListener()
        {
            _inputReciever = Instantiate(_inputRecieverPrefab, null);
            Container.Inject(_inputReciever);
            DontDestroyOnLoad(_inputReciever);
        }

        private void InitializeCoroutineRunner()
        {
            Debug.Log("Bim bim bom bom");
            _coroutineRunner = Instantiate(_coroutineRunnerPrefab, null);
            DontDestroyOnLoad(_coroutineRunner);
        }      
    }
}
