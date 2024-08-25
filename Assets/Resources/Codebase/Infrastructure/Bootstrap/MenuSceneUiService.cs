using Infrastructure;

public class MenuSceneUiService
{
    private const string _metaSceneName = "MetaGameplayScene";
    private SceneLoaderService _sceneLoaderService;
    private GameStateMachine _stateMachine;
    private MenuSceneUiContainer _menuSceneUiContainer;

    public MenuSceneUiService(SceneLoaderService sceneLoaderService, GameStateMachine stateMachine, MenuSceneUiContainer menuSceneUiContainer)
    {
        _sceneLoaderService = sceneLoaderService;
        _stateMachine = stateMachine;
        _menuSceneUiContainer = menuSceneUiContainer;

        InitButtons();
    }

    private void InitButtons()
    {
        _menuSceneUiContainer.Play.onClick.AddListener(PlayButtonHandler);
    }

    private void PlayButtonHandler()
    {
        _stateMachine.EnterState<LoadMetaSceneState, string>(_metaSceneName);
    }
}