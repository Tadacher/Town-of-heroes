using Infrastructure;
using UnityEngine;

public class MenuSceneInstaller : AbstractMonoInstaller
{
    [SerializeField] private MenuSceneUiContainer _menuSceneUiContainer;
    public override void InstallBindings()
    {
        BindService<GameSettingsService>();
        BindService<MenuSceneUiService>().NonLazy();


        BindMonobehaviour(_menuSceneUiContainer);
    }
}
