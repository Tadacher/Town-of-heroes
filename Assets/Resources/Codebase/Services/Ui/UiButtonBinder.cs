using Services.Ui;

namespace Infrastructure
{
    public class UiButtonBinder
    {     
        public UiButtonBinder(GameplayStateMachine gameplayStateMachine, GameplayCanvasContainer gameplayCanvasContainer, MapCanvasContainer mapCanvasContainer)
        {
            gameplayCanvasContainer.ToMapButton.onClick.AddListener(gameplayStateMachine.EnterState<MapState>);
            mapCanvasContainer.ToBattleFieldBtn.onClick.AddListener(gameplayStateMachine.EnterState<BattleField>);
        }
    }
}