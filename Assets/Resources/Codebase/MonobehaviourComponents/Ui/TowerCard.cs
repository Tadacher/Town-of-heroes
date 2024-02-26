using Infrastructure;
using Services.Input;
using Services.TowerBuilding;
using Services.Ui;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerCard : MonoBehaviour, IPoolableObject, IPointerDownHandler
{
    public Type TowerType => _towerType;
    //public Type WorldCellType => _worldCellType;


    //external
    [SerializeField] private RectTransform _cardImageTransform;
    [SerializeField] private Sprite _imageAsTower;
    [SerializeField] private Sprite _imageAsCell;
    [SerializeField] private Image _image;
    [SerializeField] private float _speed = 650f;

    //dependencies

    private IObjectPooler _pooler;

    //TowerPlacing
    private TowerBuildingService _towerInstantiationService;
    private Type _towerType;

    //WorldCellPlacing
    private WorldCellBuildingService _worldCellInstantiationService;
    private Type _worldCellType;

    //Input
    private IShiftEventProvider _shiftEventProvider;
    private AbstractInputService _inputService;

    //GUI
    private CardInfoUiService _cardInfoUIService;
    private TowerCardInfoConfig _towerCardInfoConfig;
    private WorldCellCardInfoConfig _worldCellCardInfoConfig;

    //internal
    private IExitableState _currentState;
    private GameplayStateMachine _gameplayStateMachine;
    private Coroutine _switchStateCoroutine;
    private GameObject _gameObject;

    private bool _battlefieldStated;
    private bool _readyToUse;

    public void Initialize(TowerBuildingService towerBuildingService,
                           WorldCellBuildingService worldCellBuildingService,
                           GameplayStateMachine gameplayStateMachine,
                           IObjectPooler pooler,
                           IShiftEventProvider shiftEventProvider,
                           CardInfoUiService cardInfoUiService,
                           AbstractInputService inputService,
                           TowerCardInfoConfig towerCardInfoConfig,
                           WorldCellCardInfoConfig worldCellCardInfoConfig,
                           Sprite worldCellSprite,
                           Sprite towerSprite,
                           Type towerType,
                           Type worldCellType)
    {
        _gameObject = this.gameObject;
        _towerCardInfoConfig = towerCardInfoConfig;
        _worldCellCardInfoConfig = worldCellCardInfoConfig;
        _shiftEventProvider = shiftEventProvider;
        _gameplayStateMachine = gameplayStateMachine;
        _inputService = inputService;
        SubscribeToGameStateChange();
        SubscribeToShiftEvent();
        _pooler = pooler;

        _cardInfoUIService = cardInfoUiService;

        _towerInstantiationService = towerBuildingService;
        _towerType = towerType;

        _worldCellInstantiationService = worldCellBuildingService;
        _worldCellType = worldCellType;
        _imageAsTower = towerSprite;
        _imageAsCell = worldCellSprite;
        SetGameState();
        SetImageAsState();
    }

    public TowerCard ReInitialize()
    {
        SubscribeToGameStateChange();
        SubscribeToShiftEvent();

        SetGameState();
        SetImageAsState();
        return this;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (_inputService.LeftMouseDown())
        {
            if (_battlefieldStated)
                InstantiateTowerGhost();
            else
                InstantiateWorldCellGhost();
            gameObject.SetActive(false);
        }
        else if (_inputService.RightMouseDown())
        {
            if (_image.sprite == _imageAsTower)
                _cardInfoUIService.ShowAsTower(_towerCardInfoConfig);
            else
                _cardInfoUIService.ShowAsWorldCell(_worldCellCardInfoConfig);
        }
    }

    #region INPUT_SHIFT
    private void SubscribeToShiftEvent()
    {
        _shiftEventProvider.OnShiftDown += OnShitDownHandler;
        _shiftEventProvider.OnShiftUp += OnShiftUpHandler;
    }
    private void UnsubscribeToShiftEvent()
    {
        _shiftEventProvider.OnShiftDown -= OnShitDownHandler;
        _shiftEventProvider.OnShiftUp -= OnShiftUpHandler;
    }

    private void OnShitDownHandler()
    {
        if (!_readyToUse)
            return;

        switch (_gameplayStateMachine.ActiveState)
        {
            case BattleField battleField:
                SetImageAsCell();
                break;
            case Map map: 
                SetTImageAsTower();
            break;
        }
    }
    private void OnShiftUpHandler()
    {
        SetImageAsState();    
    }
    #endregion

    #region GAME_STATE_HANDLERS
    private void SubscribeToGameStateChange() => _gameplayStateMachine.OnStateChanged += OnGameplayStateChanged;
    private void UnsubscribeToGameStateChange() => _gameplayStateMachine.OnStateChanged -= OnGameplayStateChanged;
    protected void OnGameplayStateChanged(IExitableState state)
    {
        if (!_gameObject.activeSelf)
            return;

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
        if (_switchStateCoroutine != null)
            StopCoroutine(_switchStateCoroutine);
        _switchStateCoroutine = StartCoroutine(SwitchState());
    }
    private void SwitchToMapState()
    {
        _battlefieldStated = false;
        ResetScale();
        if (_switchStateCoroutine != null)
            StopCoroutine(_switchStateCoroutine);
        _switchStateCoroutine = StartCoroutine(SwitchState());
    }
    private void SetGameState()
    {
        if (_gameplayStateMachine.ActiveState is BattleField)
            _battlefieldStated = true;
        else
            _battlefieldStated = false;
    }
    #endregion

    #region SWITCH_STATE_ROUTINE
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
    #endregion

    #region UTIL
    private void InstantiateWorldCellGhost() => _worldCellInstantiationService.InstantiateWorldCellFromCard(this, _worldCellType);

    private void InstantiateTowerGhost() => _towerInstantiationService.InstantiateTowerFromCard(this, _towerType);

    public void ReturnToPool()
    {
        UnsubscribeToGameStateChange();
        UnsubscribeToShiftEvent();
        _pooler.ReturnToPool(this);
    }

    public void StartTranslationCoroutine(Transform cardParent) => StartCoroutine(TranslationCoroutine(cardParent));

    private IEnumerator TranslationCoroutine(Transform cardParent)
    {
        
        _readyToUse = false;
        RectTransform transform = GetComponent<RectTransform>();
        RectTransform targetTransform = cardParent.GetComponent<RectTransform>();

        transform.localPosition = Vector3.right * Screen.width;

        Vector3 direction = (targetTransform.anchoredPosition - transform.anchoredPosition).normalized;
        while(transform.anchoredPosition.x > Screen.width/2)
        {
            Debug.Log($"{Screen.width/2} > {transform.anchoredPosition.x}");
            transform.localPosition += _speed * Time.deltaTime * direction;
            yield return null;
        }
        transform.SetParent(cardParent);
        _readyToUse = true;
    }

    ~TowerCard()
    {
        UnsubscribeToGameStateChange();
        UnsubscribeToShiftEvent();
    }
    #endregion
}
