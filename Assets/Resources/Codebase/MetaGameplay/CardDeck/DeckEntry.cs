using Core.Towers;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DeckEntry : MonoBehaviour, IPointerDownHandler, IPoolableObject
{
    public Type RelatedTowerType { get; private set; }
    [SerializeField] private Text _name;
    private IDeckEntryPressedReciever _reciever;
    private IObjectPooler _pooler;

    public void Initialize(IObjectPooler objectPooler, IDeckEntryPressedReciever deckEntryPressedReciever)
    {
        _pooler = objectPooler;
        _reciever = deckEntryPressedReciever;
    }
    public void ReturnToPool()
    {
        _pooler.ReturnToPool(this);
    }

    public void InitContent(ITowerInfoProvider towerInfoProvider, Type type)
    {
        _name.text = towerInfoProvider.Name;
        RelatedTowerType = type;

    }

    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (!gameObject.activeSelf)
            return;
        _reciever.DeckEntryPressed(this);
        ReturnToPool();
    }

    public void ActionOnGet()
    {
        
    }
}