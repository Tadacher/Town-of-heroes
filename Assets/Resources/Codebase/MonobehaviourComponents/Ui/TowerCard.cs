using Infrastructure;
using Services.TowerBuilding;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerCard : MonoBehaviour, IPoolableObject, IPointerDownHandler
{
    //external
    [SerializeField] private RectTransform _cardImageTransform;
    [SerializeField] private Sprite _imageAsTower;
    [SerializeField] private Sprite _imageAsCell;
    [SerializeField] private Image _image;
    //

    //dependencies

    private IObjectPooler _pooler;
    //TowerPlacing
    private TowerBuildingService _towerInstantiationService;
    private Type _towerType;
    //WorldCellPlacing
    private WorldCellBuildingService _worldCellInstantiationService;
    private Type _worldCellType;

    private IExitableState _currentState;
    private GameplayStateMachine _gameplayStateMachine;
    private Coroutine _swithStateCoroutine;
    private bool _battlefieldStated;

    private readonly Type _worldCellMap = typeof(GrassField);

    public void Initialize(TowerBuildingService towerBuildingService,
                           WorldCellBuildingService worldCellBuildingService,
                           GameplayStateMachine gameplayStateMachine,
                           IObjectPooler pooler,
                           Type towerType,
                           Type worldCellType)
    {
        _gameplayStateMachine = gameplayStateMachine;
        _gameplayStateMachine.OnStateChanged += OnGameplayStateChanged;
        _pooler = pooler;


        _towerInstantiationService = towerBuildingService;
        _towerType = towerType;

        _worldCellInstantiationService = worldCellBuildingService;
        _worldCellType = worldCellType; //TODO: move to getter class

        SetGameState();
        SetImageAsState();
    }
    public TowerCard ReInitialize()
    {
        SetGameState();
        SetImageAsState();
        return this;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_battlefieldStated)
            InstantiateTowerGhost();
        else
            InstantiateWorldCellGhost();

        gameObject.SetActive(false);
    }
    private void SetGameState()
    {
        if (_gameplayStateMachine.ActiveState is BattleField)
            _battlefieldStated = true;
        else
            _battlefieldStated = false;
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
        ResetScale();
        _swithStateCoroutine = StartCoroutine(SwitchState());
    }
    private void SwitchToMapState()
    {
        _battlefieldStated = false;
        ResetScale();
        _swithStateCoroutine = StartCoroutine(SwitchState());
    }
    private void ResetScale() => transform.localScale = new Vector3(1, 1, 1);
    private IEnumerator SwitchState()
    {
        while (transform.localScale.x > 0)
        {
            transform.localScale -= Vector3.right * Time.deltaTime;
            yield return null;
        }

        SetImageAsState();

        while (transform.localScale.x < 1)
        {
            transform.localScale += Vector3.right * Time.deltaTime;
            yield return null;
        }
    }
    private void SetImageAsState()
    {
        if (_battlefieldStated)
            SetTImageAsTower();
        else
            SetImageAsCell();
    }

    private void SetImageAsCell() => _image.sprite = _imageAsCell;

    private void SetTImageAsTower() => _image.sprite = _imageAsTower;

    

    private void InstantiateWorldCellGhost() => _worldCellInstantiationService.InstantiateWorldCellFromCard(this, _worldCellType);

    private void InstantiateTowerGhost() => _towerInstantiationService.InstantiateTowerFromCard(this, _towerType);

    public void ReturnToPool() =>
        _pooler.ReturnToPool(this);
}
