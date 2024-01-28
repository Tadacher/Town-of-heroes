using Services.Ui;

namespace Infrastructure
{
    public class UiService
    {
        private GameplayCanvasContainer _gameplayCanvasContainer;
        private MapCanvasContainer _mapCanvasContainer;
        
        
        public UiService(GameplayCanvasContainer gameplayCanvasContainer, MapCanvasContainer mapCanvasContainer)
        {
            _gameplayCanvasContainer = gameplayCanvasContainer;
            _mapCanvasContainer = mapCanvasContainer;
        }

        public void SwitchToGameplayCanvas()
        {
            _gameplayCanvasContainer.gameObject.SetActive(true);
            _mapCanvasContainer.gameObject.SetActive(false);
        }
        public void SwitchToMapCanvas ()
        {
            _gameplayCanvasContainer.gameObject.SetActive(false);
            _mapCanvasContainer.gameObject.SetActive(true);
        }
    }
}