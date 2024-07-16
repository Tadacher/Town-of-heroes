using Services.Ui;

namespace Infrastructure
{
    public class CoreGameplaySceneUiService
    {
        private GameplayCanvasContainer _gameplayCanvasContainer;
        private MapCanvasContainer _mapCanvasContainer;

        public CoreGameplaySceneUiService(GameplayCanvasContainer gameplayCanvasContainer, MapCanvasContainer mapCanvasContainer)
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
        public void SetCastleHP(int HPCastle) =>
            _gameplayCanvasContainer.CastleHealthBarText.text = HPCastle.ToString();
    }
}