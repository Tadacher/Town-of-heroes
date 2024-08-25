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
        [SerializeField] private DialogSystemView _dialogSystemViewPrefab;
        private CoroutineProcessorService _coroutineRunner;
        private InputReciever _inputReciever;
        public override void InstallBindings()
        {
            InitAndBindConfigs();
            BindInterfacesAndSelfto<DesctopInput, AbstractInputService>();

            InitializeCoroutineRunner();
            InitializeInputReciever();

            BindInterfaceFromInstance<ICoroutineRunner, CoroutineProcessorService>(_coroutineRunner);


            BindService<SceneLoaderService>();
            BindService<GameStateFactory>();
            BindService<DialogService>();

            
            ConstrucntAndBindDialogView();
            //saveload
            BindService<MetaCitySaveLoader>();
            BindService<CardDeckSaveLoader>();
            BindService<ResourcesSaveLoader>();
            //

            BindService<ResourceService>().NonLazy();

            BindService<GameStateMachine>().NonLazy();
            Debug.Log("BOOTSTRAP INSTALLER DONE");
        }

        private void ConstrucntAndBindDialogView()
        {
            DialogSystemView view = Instantiate(_dialogSystemViewPrefab);
            BindMonobehaviour(view);
            DontDestroyOnLoad(view);
        }

        private void InitializeInputReciever()
        {
            _inputReciever = Instantiate(_inputRecieverPrefab, null);
            Container.Inject(_inputReciever);
            DontDestroyOnLoad(_inputReciever);
        }

        private void InitializeCoroutineRunner()
        {
            _coroutineRunner = Instantiate(_coroutineRunnerPrefab, null);
            DontDestroyOnLoad(_coroutineRunner);
        }
    }
}
