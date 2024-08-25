using Services.GlobalMap;

namespace Infrastructure
{
    public class MapState : IState
    {
        private CameraPositionService _cameraPositionService;
        private readonly CoreGameplaySceneUiService _uiService;
        private AbstractStateMachine stateMachine;
        private WorldCellGridService _worldCellGridService;

        private bool _gridInittied;
        public MapState (CameraPositionService cameraPositionService, CoreGameplaySceneUiService uiService, WorldCellGridService worldCellGridService)
        {
            _cameraPositionService = cameraPositionService;
            _uiService = uiService;
            _worldCellGridService = worldCellGridService;
        }

        public void Enter()
        {
           _cameraPositionService.SetCamToMap();
           _uiService.SwitchToMapCanvas();
            if(!_gridInittied)
            {
                _gridInittied = true;
                _worldCellGridService.Init();
            }
        }

        public void Exit()
        {
            return;
        }

        public IExitableState Init(AbstractStateMachine gameStateMachine)
        {
            stateMachine = gameStateMachine;
            return this;
        }
    }
}