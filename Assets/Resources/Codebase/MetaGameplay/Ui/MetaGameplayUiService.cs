using Metagameplay.Ui;

public class MetaGameplayUiService
{
    private MetaUiContainer _container;
    public MetaGameplayUiService(MetaUiContainer container)
    {
        _container = container;
        InitBuildMenuUi();
        InitGeneralUi();
    }

    private void InitGeneralUi() => _container.BuildMenuButton.onClick.AddListener(EnableBuildingMenu);
    private void InitBuildMenuUi() => _container.BuildMenuUiContainer.ExitButton.onClick.AddListener(DisableBuildMenu);
    public void EnableBuildingMenu() => _container.BuildMenuUiContainer.gameObject.SetActive(true);
    public void DisableBuildMenu() => _container.BuildMenuUiContainer.gameObject.SetActive(false);
}
