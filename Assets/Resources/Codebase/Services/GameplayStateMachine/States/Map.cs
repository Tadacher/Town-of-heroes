namespace Infrastructure
{
    public class Map : IState
    {
        private CameraPositionService _cameraPositionService;
        private readonly UiService _uiService;

        public Map (CameraPositionService cameraPositionService, UiService uiService)
        {
            _cameraPositionService = cameraPositionService;
            _uiService = uiService;
        }

        public void Enter()
        {
           _cameraPositionService.SetCamToMap();
           _uiService.SwitchToMapCanvas();
        }

        public void Exit()
        {
            return;
        }
    }
}