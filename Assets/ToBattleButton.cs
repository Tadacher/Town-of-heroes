using Infrastructure;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ToBattleButton : MonoBehaviour
{
    private const string _gameplaySceneName = "CoreGameplayScene";
    [SerializeField] private Button _button;

    private GameStateMachine _gameStateMachine;
    private MetaGridSevice _metaGridSevice;
    MetaCitySaveLoader _citySaveLoader;

    [Inject]
    public void Init(GameStateMachine gameStateMachine, MetaGridSevice metaGridSevice, MetaCitySaveLoader metaCitySaveLoader)
    {
        _metaGridSevice = metaGridSevice;
        _citySaveLoader = metaCitySaveLoader;
        _gameStateMachine = gameStateMachine;
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _citySaveLoader.Save(_metaGridSevice.GetSaveData());
        _gameStateMachine.EnterState<LoadInitialLevelState, string>(_gameplaySceneName);
    }
}
