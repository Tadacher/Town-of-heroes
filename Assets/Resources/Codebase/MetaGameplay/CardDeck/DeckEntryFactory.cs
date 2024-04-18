using Metagameplay.Ui;
using Services.Factories;
using System;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class DeckEntryFactory : AbstractPoolerFactory<DeckEntry>
{
    private Transform _parent;
    private DeckEntry _prefab;
    private IDeckEntryPressedReciever _pressedReciever;

    public DeckEntryFactory(DiContainer diContainer, MetaUiContainer metaUiContainer, DeckEntry prefab) : base(diContainer)
    {
        _parent = metaUiContainer.DeckEditingUiContainer.EntryParent;
        _prefab = prefab;
    }
    public void InjectReciever(IDeckEntryPressedReciever deckEntryPressedReciever) => _pressedReciever = deckEntryPressedReciever;

    protected override void ActionOnDestroy(DeckEntry poolable)
    {
        
    }

    protected override void ActionOnGet(DeckEntry poolable)
    {
        poolable.gameObject.SetActive(true);
    }

    protected override void ActionOnRelease(DeckEntry poolale)
    {
        poolale.gameObject.SetActive(false);
    }

    protected override DeckEntry CreateNew()
    {
        DeckEntry entry = _container.InstantiatePrefabForComponent<DeckEntry>(_prefab, _parent);
        entry.Initialize(
            deckEntryPressedReciever:_pressedReciever,
            objectPooler: this);
        return entry;
    }

}
