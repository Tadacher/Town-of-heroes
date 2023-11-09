using Services.TowerBuilding;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

public class TowerCard : MonoBehaviour, IPoolableObject, IPointerDownHandler
{
    //external
    [SerializeField] private RectTransform _cardImageTransform;
    //

    //dependencies
    private IObjectPooler _pooler;
    private TowerBuildingService _instantiationService;
    private Type _towerType;
    //


    public void Initialize(TowerBuildingService service, IObjectPooler pooler, Type towerType)
    {
        _pooler = pooler;
        _instantiationService = service;
        _towerType = towerType;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        _instantiationService.InstantiateTowerFromCard(this, _towerType);
        gameObject.SetActive(false);
    }

    public void ReturnToPool() =>
        _pooler.ReturnToPool(this);
}
