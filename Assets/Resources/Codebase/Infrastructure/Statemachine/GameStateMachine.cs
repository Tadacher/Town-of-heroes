using Infrastructure;
using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public class GameStateMachine : AbstractStateMachine
    {

        public GameStateMachine(GameStateFactory stateFactory)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = stateFactory.GetBootsrapState().Init(this),
                [typeof(LoadInitialLevelState)] = stateFactory.GetPayloadedState<string, LoadInitialLevelState>().Init(this),
                [typeof(MetaSceneState)] = stateFactory.GetState<MetaSceneState>().Init(this),
                [typeof(CoreSceneState)] = stateFactory.GetState<CoreSceneState>().Init(this),
            };
            EnterState<BootstrapState>();
        }
    }
}
