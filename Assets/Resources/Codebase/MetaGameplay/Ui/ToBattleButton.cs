using Infrastructure;
using Services.CardGeneration;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ToBattleButton : MonoBehaviour
{
    private const string _gameplaySceneName = "CoreGameplayScene";
    [SerializeField] private Button _button;

    private GameStateMachine _gameStateMachine;
    private MetaGridSevice _metaGridSevice;
    private MetaCitySaveLoader _citySaveLoader;
    private CardDeckSaveLoader _cardDeckSaveLoader;
    private CardDeckService _cardDeckService;

    [Inject]
    public void Init(GameStateMachine gameStateMachine, MetaGridSevice metaGridSevice, MetaCitySaveLoader metaCitySaveLoader, CardDeckSaveLoader cardDeckSaveLoader, CardDeckService cardDeckService)
    {
        _metaGridSevice = metaGridSevice;
        _citySaveLoader = metaCitySaveLoader;
        _gameStateMachine = gameStateMachine;
        _cardDeckSaveLoader = cardDeckSaveLoader;
        _cardDeckService = cardDeckService;

        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _citySaveLoader.Save(_metaGridSevice.GetSaveData());
        _cardDeckSaveLoader.Save();

        _gameStateMachine.EnterState<LoadMetaSceneState, string>(_gameplaySceneName);
    }
}
