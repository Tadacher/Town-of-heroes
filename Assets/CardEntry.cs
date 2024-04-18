using Core.Towers;
using Services.Input;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
/// <summary>
/// Card entry in deck editing menu
/// </summary>
[RequireComponent(typeof(RectPointerFollower))]
public class CardEntry : MonoBehaviour, IPointerDownHandler, IPoolableObject
{
    public Type RelatedTowerType { get; private set; }
    [SerializeField] private RectPointerFollower _pointerFollower;
    [SerializeField] private Text _header;
    //TODO: add content, text fields, etc

    private AbstractInputService _inputService;
    private RectTransform _parent;
    private RectTransform _canvasTransform;
    private IEntryInserter _deckEntryInserter;
    private IObjectPooler _pooler;


    public void Initialize(AbstractInputService inputService,
                           IEntryInserter deckEntryInserter,
                           IObjectPooler objectPooler,
                           RectTransform parentTransform,
                           RectTransform canvasTransform)
    {
        _inputService = inputService;
        _pooler = objectPooler;
        _deckEntryInserter = deckEntryInserter;
        _parent = parentTransform;
        _canvasTransform = canvasTransform;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        AwaitForRelease();
    }

    public void AwaitForRelease()
    {
        StartFollowPointer();
        _inputService.OnPointerUp += TryToReleaseAtDeck;
    }

    public void ReturnToPool()
    {
        _pooler.ReturnToPool(this);
        _inputService.OnPointerUp -= TryToReleaseAtDeck;
    }
    public void InitContent(ITowerInfoProvider infoProvider, Type relatedTowerType)
    {
        _header.text = infoProvider.Name;
        RelatedTowerType = relatedTowerType;
    }

    private void TryToReleaseAtDeck()
    {
        StopFollowingpoiner();

        if (_deckEntryInserter.TryInsertToDeck(this))
            ReturnToPool();
        else
            ReturnToParent();
    }

    private void ReturnToParent()
    {
        transform.SetParent(_parent);
        _inputService.OnPointerUp -= TryToReleaseAtDeck;
    }

    private void StartFollowPointer()
    {
        _pointerFollower.enabled = true;
        transform.SetParent(_canvasTransform);
    }

    private void StopFollowingpoiner()
    {
        _pointerFollower.enabled = false;
        transform.SetParent(_parent);
    }
}
