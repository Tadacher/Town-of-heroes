using Assets.Resources.Codebase.Infrastructure;
using System;
using UnityEngine;
using Zenject;

namespace Infrastructure
{
    public class BootstrapInstaller : AbstractMonoInstaller
    {
        [SerializeField] CoroutineProcessorService _coroutineRunnerPrefab;
         CoroutineProcessorService _coroutineRunner;
        public override void InstallBindings()
        {
            InitializeCoroutineRunner();
            InstallInterfaceBindingFromInstance<ICoroutineRunner, CoroutineProcessorService>(_coroutineRunner);
            InstallBinding<SceneLoaderService>();
            InstallBindingNonLazy<GameStateMachine>();
            Debug.Log("DONE");
        }

        private void InitializeCoroutineRunner()
        {
            Debug.Log("Lol");
            _coroutineRunner = Instantiate(_coroutineRunnerPrefab, null);
            DontDestroyOnLoad(_coroutineRunner);
        }

        private void InstallBindingNonLazy<TBinding>() => 
            Container.Bind<TBinding>().AsSingle().NonLazy();
    }
}
