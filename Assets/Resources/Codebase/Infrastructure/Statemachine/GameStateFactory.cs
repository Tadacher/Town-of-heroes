using System;
using Zenject;

namespace Infrastructure
{
    public class GameStateFactory
    {
        private DiContainer _container;
        public GameStateFactory(DiContainer container) => _container = container;

        public IExitableState GetState<TState>() where TState : IExitableState => 
            (IExitableState)_container.Instantiate(typeof(TState));

        public IPayloadedState<TPayload> GetPayloadedState<TPayload, TState>() where TState : IPayloadedState<TPayload> => 
            (IPayloadedState<TPayload>)_container.Instantiate(typeof(TState));

        public BootstrapState GetBootsrapState()
        {
            var state = new BootstrapState(_container.Resolve<SceneLoaderService>());
            return state;
        }
    }
}
