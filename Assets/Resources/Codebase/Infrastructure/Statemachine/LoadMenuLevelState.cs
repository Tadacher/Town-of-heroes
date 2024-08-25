using System;

namespace Infrastructure
{
    public class LoadMenuLevelState : IPayloadedState<string>
    {
        AbstractStateMachine _statemachine;
        SceneLoaderService _sceneLoaderService;

        public LoadMenuLevelState(SceneLoaderService sceneLoaderService)
        {
            _sceneLoaderService = sceneLoaderService;
        }

        public void Enter()
        {
            
        }

        public void Enter(string payload)
        {
            _sceneLoaderService.StartLoadSceneCoroutine(payload, EnterLevelState);
        }

        private void EnterLevelState()
        {
            _statemachine.EnterState<MenuLevelState>();
        }

        public void Exit()
        {
            
        }

        public IExitableState Init(AbstractStateMachine gameStateMachine)
        {
            _statemachine = gameStateMachine;
            return this;
        }
    }
}