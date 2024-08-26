using Metagameplay.Ui;
using Services.Factories;
using System;
using System.ComponentModel;
using UnityEngine;
using Zenject;

public class DeckEntryFactory : MonobehaviourAbstractPoolerFactory<DeckEntry>
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

    protected override DeckEntry CreateNew()
    {
        DeckEntry entry = _container.InstantiatePrefabForComponent<DeckEntry>(_prefab, _parent);
        entry.Initialize(
            deckEntryPressedReciever:_pressedReciever,
            objectPooler: this);
        return entry;
    }

}
