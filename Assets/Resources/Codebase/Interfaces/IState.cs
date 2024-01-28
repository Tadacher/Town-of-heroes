namespace Infrastructure
{
    public interface IState : IExitableState
    {
        public void Enter();
    }
    public interface IPayloadedState<TPayload> : IExitableState
    {
        public void Enter(TPayload payload);
    }
}
