namespace Infrastructure
{
    internal class LoadCoreSceneState : IPayloadedState<string>
    {
        private SceneLoaderService _sceneLoaderService;
        private AbstractStateMachine _gameStateMachine;

        public LoadCoreSceneState(SceneLoaderService sceneLoaderService)
        {
            _sceneLoaderService = sceneLoaderService;
        }

        void IPayloadedState<string>.Enter(string payload) =>
             _sceneLoaderService.StartLoadSceneCoroutine(payload, EnterLevelState);

        private void EnterLevelState()
        {
            _gameStateMachine.EnterState<CoreSceneState>();
        }

        public void Exit()
        {
            
        }

        public IExitableState Init(AbstractStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            return this;
        }
    }
}
