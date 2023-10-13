using Assets.Resources.Codebase.Infrastructure;
using Services.Input;
using System;
using Unity.VisualScripting;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : AbstractMonoInstaller
    {
        [SerializeField] CoroutineProcessorService _coroutineRunnerPrefab;
        [SerializeField] InputReciever _inputRecieverPrefab;
         CoroutineProcessorService _coroutineRunner;
        public override void InstallBindings()
        {
            BindInterfacesAndSelft<DesctopInput>();

            InitializeCoroutineRunner();
            InitializeInputListener();

            InstallInterfaceBindingFromInstance<ICoroutineRunner, CoroutineProcessorService>(_coroutineRunner);

            BindService<SceneLoaderService>();
            BindService<GameStateMachine>()
                .NonLazy();
            Debug.Log("DONE");
        }

        private void InitializeInputListener()
        {
            var inputReciever = Instantiate(_inputRecieverPrefab, null);
            DontDestroyOnLoad(inputReciever);
        }

        private void InitializeCoroutineRunner()
        {
            Debug.Log("Lol");
            _coroutineRunner = Instantiate(_coroutineRunnerPrefab, null);
            DontDestroyOnLoad(_coroutineRunner);
        }

        
    }
}
