namespace Infrastructure
{
    public interface IExitableState
    {
        public void Exit();
        IExitableState Init(AbstractStateMachine gameStateMachine);
    }
}
