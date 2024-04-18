using Metagameplay.Ui;
using Services.Factories;
using Services.Input;
using UnityEngine;
using Zenject;

public class CardEntryFactory : AbstractPoolerFactory<CardEntry>
{
    private CardEntry _prefab;
    private Transform _parent;
    private Transform _canvasParent;
    private CardDeckEditingService _cardDeckEditingservice;
    public CardEntryFactory(DiContainer diContainer, CardEntry prefab, MetaUiContainer metaUiContainer) : base(diContainer)
    {
        _prefab = prefab;
        _parent = metaUiContainer.DeckEditingUiContainer.CardParent;
        _canvasParent = metaUiContainer.MetaUiCanvasTransform;

    }
    public void Init(CardDeckEditingService service)
    {
        _cardDeckEditingservice = service;
    }
    protected override void ActionOnDestroy(CardEntry poolable)
    {
        Object.Destroy(poolable.gameObject);
    }

    protected override void ActionOnGet(CardEntry poolable)
    {
        poolable.gameObject.SetActive(true);
    }

    protected override void ActionOnRelease(CardEntry type)
    {
        type.gameObject.SetActive(false);
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
