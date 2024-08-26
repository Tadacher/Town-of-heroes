using Services.Input;
using UnityEngine;
public class MonsterInfoServiceIngame
{
    private MonsterInfoView _view;
    private readonly AbstractInputService _abstractInputService;

    private IHitpointInfoProvider _hitpointInfoProvider;

    public MonsterInfoServiceIngame(MonsterInfoView view, AbstractInputService abstractInputService)
    {
        _view = view;
        _abstractInputService = abstractInputService;
    }

    public void Show(AbstractEnemy abstractEnemy, IHitpointInfoProvider hitpointInfo)
    {
        _view.Title.text = abstractEnemy.EnemyName;
        _view.Description.text = "not yet";

        _hitpointInfoProvider = hitpointInfo;


       // _view.RectTransform.anchoredPosition = _abstractInputService.GetPointerPositionScreen();
        _view.gameObject.SetActive(true);


        RedrawBar();
        _hitpointInfoProvider.OnHealthChanged += RedrawBar;
        _abstractInputService.OnRightButtonUp += HideWindow;
    }

    private void RedrawBar()
    {
        Debug.Log($"{_hitpointInfoProvider.CurrentHealth}/{_hitpointInfoProvider.MaxHealth}");
        float health = _hitpointInfoProvider.CurrentHealth;
        if (health < 0)
            health =0;
        _view.HealthBar.fillAmount = health / _hitpointInfoProvider.MaxHealth;
        _view.Health.text = $"{_hitpointInfoProvider.CurrentHealth}/{_hitpointInfoProvider.MaxHealth}";
    }

    private void HideWindow()
    {
        if(_hitpointInfoProvider != null)
            _hitpointInfoProvider.OnHealthChanged -= RedrawBar;

        _view.gameObject.SetActive(false);
        _abstractInputService.OnRightButtonUp -= HideWindow;
    }
}
