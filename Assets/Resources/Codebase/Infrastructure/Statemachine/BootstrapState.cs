using Infrastructure;
using System;

namespace Infrastructure
{
    public class BootstrapState : IState
    {
        private const string _initialSceneName = "InitialScene";
        private const string _coreGameplayScene = "CoreGameplayScene";
        private const string _metaGameplayState = "MetaGameplayScene";
        private const string _menuScene = "MenuScene";
        private AbstractStateMachine _gameStateMachine;
        private SceneLoaderService _sceneLoaderService;
        public BootstrapState(SceneLoaderService sceneLoaderService)
        {
            _sceneLoaderService = sceneLoaderService;
        }
        public IExitableState Init(AbstractStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            return this;
        }

        public void Enter()
        {
            _sceneLoaderService.StartLoadSceneCoroutine(_initialSceneName, onloaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() => 
            _gameStateMachine.EnterState<LoadMenuLevelState, string>(_menuScene);

        private void RegisterServices()
        {
        }

        public void Exit()
        {
        }
    }
}
