using System;
using System.Collections.Generic;
namespace Infrastructure
{
    public class AbstractStateMachine
    {
        public IExitableState ActiveState => _activeState;
        protected IExitableState _activeState;

        protected Dictionary<Type, IExitableState> _states;

        public void EnterState<TState>() where TState : class, IState => 
            ChangeState<TState>().Enter();
        public void EnterState<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload> => 
            ChangeState<TState>().Enter(payload);
        protected virtual TState ChangeState<TState>() where TState : class, IExitableState
        {
            _activeState?.Exit();
            TState state = GetState<TState>();
            _activeState = state;
            return state;
        }

        protected TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}