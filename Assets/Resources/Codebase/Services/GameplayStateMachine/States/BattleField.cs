namespace Infrastructure
{
    public class BattleField : IState
    {
        private CameraPositionService _cameraPositionService;
        private readonly CoreGameplaySceneUiService _uiService;
        private AbstractStateMachine _gameStateMachine;

        public BattleField(CameraPositionService cameraPositionService, CoreGameplaySceneUiService uiService)
        {
            _cameraPositionService = cameraPositionService;
            _uiService = uiService;
        }

        public void Enter()
        {
            _cameraPositionService.SetCamToBattlefield();
            _uiService.SwitchToGameplayCanvas();
        }

        public void Exit()
        {
            return;
        }

        public IExitableState Init(AbstractStateMachine gameStateMachine)
        {
           _gameStateMachine = gameStateMachine;
            return this;
        }
    }
}