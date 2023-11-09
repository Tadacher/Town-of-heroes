namespace Infrastructure
{
    public class BattleField : IState
    {
        private CameraPositionService _cameraPositionService;
        private readonly UiService _uiService;

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
    }
}