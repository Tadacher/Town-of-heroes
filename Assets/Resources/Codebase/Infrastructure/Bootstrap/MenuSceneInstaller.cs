using Infrastructure;
using UnityEngine;

public class MenuSceneInstaller : AbstractMonoInstaller
{
    [SerializeField] private MenuSceneUiContainer _menuSceneUiContainer;

    public override void InstallBindings()
    {
        BindMonobehaviour(_menuSceneUiContainer);
        BindService<MenuSceneUiService>();
    }
}

public class MenuSceneUiService
{
    private MenuSceneUiContainer _menuSceneUiContainer;
    private GameStateMachine _gameStateMachine;

    public MenuSceneUiService(MenuSceneUiContainer menuSceneUiContainer)
    {
        _menuSceneUiContainer = menuSceneUiContainer;

        _menuSceneUiContainer.StartButton.onClick.AddListener(() => _gameStateMachine.EnterState<LoadMetaGameplaySceneState, string>("MetaGameplayScene"));
    }
}