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
                [typeof(LoadMetaSceneState)] = stateFactory.GetPayloadedState<string, LoadMetaSceneState>().Init(this),
                [typeof(MetaSceneState)] = stateFactory.GetState<MetaSceneState>().Init(this),
                [typeof(CoreSceneState)] = stateFactory.GetState<CoreSceneState>().Init(this),
                [typeof(LoadMenuLevelState)] = stateFactory.GetPayloadedState<string, LoadMenuLevelState>().Init(this),
                [typeof(MenuLevelState)] = stateFactory.GetState<MenuLevelState>().Init(this),
                [typeof(LoadCoreGameplaySceneState)] = stateFactory.GetState<LoadCoreGameplaySceneState>().Init(this),
            };
            EnterState<BootstrapState>();
        }
    }
}
