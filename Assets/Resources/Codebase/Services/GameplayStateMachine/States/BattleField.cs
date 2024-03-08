namespace Infrastructure
{
    public class BattleField : IState
    {
        private CameraPositionService _cameraPositionService;
        private readonly UiService _uiService;
        private AbstractStateMachine _gameStateMachine;

        public BattleField(CameraPositionService cameraPositionService, UiService uiService)
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