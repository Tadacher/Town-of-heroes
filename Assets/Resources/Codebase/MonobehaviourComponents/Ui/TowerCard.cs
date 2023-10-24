using Services.TowerBuilding;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class TowerCard : MonoBehaviour, IPoolableObject, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{
    //external
    [SerializeField] private RectTransform _cardImageTransform;
    [SerializeField] private float _popupDistance;
    [SerializeField] private float _popupSpeed;
    [SerializeField] private IObjectPooler _pooler;
    //

    //internal
    private Coroutine _popupCoroutine;
    //

    //dependencies
    private TowerBuildingService _instantiationService;
    private Type _towerType;
    //

   // [Inject]
    public void Initialize(TowerBuildingService service, Type towerType)
    {
        _instantiationService = service;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_popupCoroutine != null) 
            StopCoroutine(_popupCoroutine);

        _popupCoroutine = StartCoroutine(PopupCoroutine());
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (_popupCoroutine != null)
            StopCoroutine(_popupCoroutine);

        _popupCoroutine = StartCoroutine(PopdownCoroutine());
    }

    private IEnumerator PopupCoroutine()
    {
        while(_cardImageTransform.localPosition.y < _popupDistance) 
        {
            _cardImageTransform.localPosition += Vector3.up * _popupSpeed * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator PopdownCoroutine()
    {
        while (_cardImageTransform.localPosition.y >0)
        {
            _cardImageTransform.localPosition -= Vector3.up * _popupSpeed * Time.deltaTime;
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
