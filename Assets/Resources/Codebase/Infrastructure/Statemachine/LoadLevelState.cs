using Infrastructure;

namespace Assets.Resources.Codebase.Infrastructure
{
    public class LoadLevelState : IPayloadedState<string>
    {
        private const string _GameplaysceneName = "CoreGameplayScene";
        private GameStateMachine _gameStateMachine;
        private SceneLoaderService _sceneLoaderService;

        public LoadLevelState(GameStateMachine gameStateMachine, SceneLoaderService sceneLoaderService)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoaderService = sceneLoaderService;
        }


        void IPayloadedState<string>.Enter(string payload) => 
            _sceneLoaderService.StartLoadSceneCoroutine(payload);

        void IExitableState.Exit()
        {
        }
    }
}
