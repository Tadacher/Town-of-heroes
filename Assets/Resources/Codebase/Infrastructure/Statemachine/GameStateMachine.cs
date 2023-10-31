using Infrastructure;
using System;
using System.Collections.Generic;

namespace Assets.Resources.Codebase.Infrastructure
{
    public class GameStateMachine
    {
        
        private Dictionary<Type, IExitableState> _states;
        private static IExitableState _activeState;

        public GameStateMachine(SceneLoaderService sceneLoaderService)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoaderService),
                [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoaderService)
            };
            EnterState<BootstrapState>();
        }

        public void EnterState<TState>() where TState : class, IState => 
            ChangeState<TState>().Enter();
        public void EnterState<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> => 
            ChangeState<TState>().Enter(payload);

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }
    }
}
