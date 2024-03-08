using Infrastructure;
using System;

namespace Infrastructure
{
    public class LoadInitialLevelState : IPayloadedState<string>
    {
        private const string _GameplaysceneName = "CoreGameplayScene";
        private AbstractStateMachine _gameStateMachine;
        private SceneLoaderService _sceneLoaderService;

        public LoadInitialLevelState(SceneLoaderService sceneLoaderService)
        {
            _sceneLoaderService = sceneLoaderService;
        }

        public IExitableState Init(AbstractStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            return this;
        }

        void IPayloadedState<string>.Enter(string payload) => 
            _sceneLoaderService.StartLoadSceneCoroutine(payload, EnterLevelState);

        private void EnterLevelState() => _gameStateMachine.EnterState<MetaSceneState>();

        void IExitableState.Exit()
        {

        }

    }
}
