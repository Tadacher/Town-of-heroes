namespace Infrastructure
{
    public class MenuLevelState : IState
    {
        private AbstractStateMachine _stateMachine;
        private DialogService DialogService;

        public MenuLevelState(DialogService dialogService)
        {
            DialogService = dialogService;
        }

        public void Enter()
        {
            DialogService.ShowDialog("StartTheGame");
        }

        public void Exit()
        {

        }

        public IExitableState Init(AbstractStateMachine gameStateMachine)
        {
           _stateMachine = gameStateMachine;
            return this;
        }
    }
}