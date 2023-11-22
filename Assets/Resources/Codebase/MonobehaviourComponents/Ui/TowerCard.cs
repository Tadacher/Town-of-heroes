using Infrastructure;
using Services.TowerBuilding;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class TowerCard : MonoBehaviour, IPoolableObject, IPointerDownHandler
{
    //external
    [SerializeField] private RectTransform _cardImageTransform;
    //

    //dependencies
    private GameplayStateMachine _gameplayStateMachine;
    private IObjectPooler _pooler;
    private TowerBuildingService _instantiationService;
    private Type _towerType;
    private Coroutine _swithStateCoroutine;
    private bool _battlefieldStated;
    
    //


    public void Initialize(TowerBuildingService service, GameplayStateMachine gameplayStateMachine, IObjectPooler pooler, Type towerType)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _gameplayStateMachine.OnStateChanged += OnGameplayStateChanged;
        _pooler = pooler;
        _instantiationService = service;
        _towerType = towerType;
    }

    protected void OnGameplayStateChanged(IExitableState state)
    {
        switch (state)
        {
            case Map map:
                SwitchToMapState();
                break;
            case BattleField field:
                SwitchToBattlefieldState();
                break;
        }
    }
    private void SwitchToBattlefieldState()
    {
        _battlefieldStated = true;
        _swithStateCoroutine = StartCoroutine(SwitchState());
    }

    private void SwitchToMapState()
    {
        _battlefieldStated = false;
        StartCoroutine(SwitchState());
    }
    private IEnumerator SwitchState()
    {
        while (transform.localScale.x > 0)
        {
            transform.localScale -= Vector3.right * Time.deltaTime;
            yield return null;
        }

        if(_battlefieldStated) 
        { 
        
        }

        while (transform.localScale.x < 1)
        {
            transform.localScale += Vector3.right * Time.deltaTime;
            yield return null;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _instantiationService.InstantiateTowerFromCard(this, _towerType);
        gameObject.SetActive(false);
    }

    public void ReturnToPool() =>
        _pooler.ReturnToPool(this);
}
