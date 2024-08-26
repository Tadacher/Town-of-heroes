using Core.Towers;
using Services.Input;

public class TowerInfoServiceIngame
{
    private TowerInfoView _view;
    private readonly AbstractInputService _abstractInputService;

    public TowerInfoServiceIngame(TowerInfoView view, AbstractInputService abstractInputService)
    {
        _view = view;
        _abstractInputService = abstractInputService;
    }

    public void Show(AbstractTower tower, ITowerInfoProvider towerInfoProvider)
    {
        // _view.Title.text = abstractEnemy.EnemyName;

        _view.Title.text = tower.name;
        _view.Damage.text = $"Damage: {towerInfoProvider.Damage.ToString("0.0")}";
        _view.Range.text = $"Range: {towerInfoProvider.Range.ToString("0.0")}";
        _view.AttackDelay.text = $"Delay: {towerInfoProvider.Delay.ToString("0.0")}";
        _view.Level.text = $"Level: {towerInfoProvider.Level.ToString()}";

        //_view.RectTransform.anchoredPosition = _abstractInputService.GetPointerPositionScreen();
        _view.gameObject.SetActive(true);


        _abstractInputService.OnRightButtonUp += HideWindow;
    }
    private void HideWindow()
    {
        _view.gameObject.SetActive(false);
        _abstractInputService.OnRightButtonUp -= HideWindow;
    }
}
