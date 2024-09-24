using Metagameplay.Ui;
using Services.Factories;
using Services.Input;
using UnityEngine;
using Zenject;

public class CardEntryFactory : MonobehaviourAbstractPoolerFactory<CardEntry>
{
    private CardEntry _prefab;
    private Transform _parent;
    private Transform _canvasParent;
    private CardDeckEditingMenu _cardDeckEditingservice;
    public CardEntryFactory(DiContainer diContainer, CardEntry prefab, MetaUiContainer metaUiContainer) : base(diContainer)
    {
        _prefab = prefab;
        _parent = metaUiContainer.DeckEditingUiContainer.CardParent;
        _canvasParent = metaUiContainer.MetaUiCanvasTransform;

    }
    public void Init(CardDeckEditingMenu service)
    {
        _cardDeckEditingservice = service;
    }

    protected override CardEntry CreateNew()
    {
        CardEntry entry = _container.InstantiatePrefabForComponent<CardEntry>(_prefab, _parent);

        entry.Initialize(
            inputService: _container.Resolve<AbstractInputService>(),
            deckEntryInserter: _cardDeckEditingservice,
            objectPooler: this,
            parentTransform: (RectTransform)_parent,
            canvasTransform: (RectTransform)_canvasParent);

        return entry;
    }
}
