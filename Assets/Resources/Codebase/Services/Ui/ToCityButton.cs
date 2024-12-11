using Infrastructure;
using Progress;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class ToCityButton : MonoBehaviour
{
    [SerializeField] private Button _button;
    private GameStateMachine _gameStateMachine;
    private ResourcesSaveLoader _resourcesSaveLoader;
    private ResourceService _resourceService;

    [Inject]
    private void Initialize(GameStateMachine gameStateMachine, ResourcesSaveLoader resourcesSaveLoader, ResourceService resourceService)
    {
        _gameStateMachine = gameStateMachine;
        _resourcesSaveLoader = resourcesSaveLoader;
        _resourceService = resourceService;
        _button.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        _resourceService.Save();
        _gameStateMachine.EnterState<LoadMetaGameplaySceneState, string>("MetaGameplayScene");
    }
}
