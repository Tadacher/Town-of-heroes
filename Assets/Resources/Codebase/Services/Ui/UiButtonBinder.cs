using Services.Ui;

namespace Infrastructure
{
    public class UiButtonBinder
    {     
        public UiButtonBinder(GameplayStateMachine gameplayStateMachine, GameplayCanvasContainer gameplayCanvasContainer, MapCanvasContainer mapCanvasContainer)
        {
            gameplayCanvasContainer.ToMapButton.onClick.AddListener(gameplayStateMachine.EnterState<Map>);
            mapCanvasContainer.ToBattleFieldBtn.onClick.AddListener(gameplayStateMachine.EnterState<BattleField>);
        }
    }
}