namespace Infrastructure
{
    public abstract class AbstractSceneState : IState
    {
        private readonly string _sceneName;
        private AbstractStateMachine _gameStateMachine;
        protected AbstractSceneState(string sceneName)
        {
            _sceneName = sceneName;
        }

        public abstract void Enter();
        public abstract void Exit();

        public IExitableState Init(AbstractStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            return this;
        }
    }
}
