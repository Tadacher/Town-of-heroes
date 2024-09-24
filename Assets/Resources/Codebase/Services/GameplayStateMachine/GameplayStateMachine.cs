
using Services;
using Services.GlobalMap;
using System;
using System.Collections.Generic;
namespace Infrastructure
{
    public class GameplayStateMachine : AbstractStateMachine
    {
        public event Action<IExitableState> OnStateChanged;

        public GameplayStateMachine(CameraPositionService cameraPositionService, GameTimeService gameTimeService, CoreGameplaySceneUiService uiService, WorldCellGridService worldCellGridService)
        {
            _states = new Dictionary<System.Type, IExitableState>()
            {
                [typeof(BattleField)] = new BattleField(cameraPositionService, uiService),
                [typeof(MapState)] = new MapState(cameraPositionService, uiService, worldCellGridService)
            };
            EnterState<BattleField>();
            
        }

        protected override TState ChangeState<TState>()
        {
            TState state = base.ChangeState<TState>();
            OnStateChanged?.Invoke(state);
            _activeState = state;
            return state;
        }
    }
}