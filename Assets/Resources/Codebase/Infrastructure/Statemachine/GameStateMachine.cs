using Infrastructure;
using System;
using System.Collections.Generic;

namespace Infrastructure
{
    public class GameStateMachine : AbstractStateMachine
    {
        public GameStateMachine(SceneLoaderService sceneLoaderService)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoaderService),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoaderService)
            };
            EnterState<BootstrapState>();
        }
    }
}
