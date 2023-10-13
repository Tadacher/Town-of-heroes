using Core.Towers;
using Services.TowerBuilding;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class TowerCard<TTower> : MonoBehaviour, IPoolableObject, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler where TTower : AbstractTower
{
    //external
    [SerializeField] private RectTransform _cardImageTransform;
    [SerializeField] private float _popupDistance;
    [SerializeField] private float _popupSpeed;
    //

    //internal
    private Coroutine _popupCoroutine;
    //

    //dependencies
    private TowerBuildingService _instantiationService;
    //

    [Inject]
    public void Initialize(TowerBuildingService service)
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
        _instantiationService.InstantiateTowerFromCard<TTower>(this);
    }

    public void ReturnToPool()
    {
        throw new NotImplementedException();
    }
}
