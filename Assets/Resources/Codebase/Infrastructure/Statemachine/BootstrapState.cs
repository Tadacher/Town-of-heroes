using Infrastructure;
using System;

namespace Assets.Resources.Codebase.Infrastructure
{
    public class BootstrapState : IState
    {
        private const string _initialSceneName = "Initial";
        private const string _coreGameplayScene = "CoreGameplayScene";
        private GameStateMachine _gameStateMachine;
        private SceneLoaderService _sceneLoaderService;
        public BootstrapState(GameStateMachine gameStateMachine, SceneLoaderService sceneLoaderService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoaderService = sceneLoaderService;
        }

        public void Enter()
        {
            _sceneLoaderService.StartLoadSceneCoroutine(_initialSceneName, onloaded: EnterLoadLevel);
        }

        private void EnterLoadLevel() => 
            _gameStateMachine.EnterState<LoadLevelState, string>(_coreGameplayScene);

        private void RegisterServices()
        {
        }

        public void Exit()
        {
        }
    }
}
