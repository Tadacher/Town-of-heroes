using Metagameplay.Ui;
using Services.CardGeneration;
using System;
using UnityEngine;

public class CardDeckEditingMenu : IDeckEntryPressedReciever, IEntryInserter
{
    private readonly DeckEditingUiContainer _deckBuildingUiContainer;
    private readonly CardDeckSaveLoader _deckService;
    private readonly DeckEntryFactory _deckEntryFactory;
    private readonly CardEntryFactory _cardEntryFactory;
    private readonly CardDescriptionService _cardDescriptionService;
    private readonly AbstractTowerPrefabContainer _abstractTowerPrefabContainer;
    private readonly CardAvalilabilityService _cardAvalilabilityService;

    public CardDeckEditingMenu(MetaUiContainer uiContainer,
                                  CardDeckSaveLoader deckService,
                                  DeckEntryFactory deckEntryFactory,
                                  CardEntryFactory cardEntryFactory,
                                  CardDescriptionService cardDescriptionService,
                                  AbstractTowerPrefabContainer abstractTowerPrefabContainer,
                                  CardAvalilabilityService cardAvalilabilityService)
    {
        _deckBuildingUiContainer = uiContainer.DeckEditingUiContainer;
        _deckService = deckService;
        _deckEntryFactory = deckEntryFactory;
        _cardEntryFactory = cardEntryFactory;
        _cardDescriptionService = cardDescriptionService;
        _abstractTowerPrefabContainer = abstractTowerPrefabContainer;
        _cardAvalilabilityService = cardAvalilabilityService;
        InitDeckEntryFactory();
        InitCardFactory();
        InitDeckLayoutContent();
        InitCardLayoutContent();
    }

    private void InitCardFactory() => _cardEntryFactory.Init(this);
    private void InitDeckEntryFactory() => _deckEntryFactory.InjectReciever(this);

    private void InitCardLayoutContent()
    {
        foreach (Core.Towers.AbstractTower tower in _abstractTowerPrefabContainer.Towers)
        {
            if (_deckService.CardDeckSave.Contains(tower.GetType()))
                continue;

            if (!_cardAvalilabilityService.AllowedTypes.Contains(tower.GetType()))
                continue;

            CardEntry card = _cardEntryFactory.GetObject();
            card.InitContent(tower, tower.GetType());
        }
    }

    private void InitDeckLayoutContent()
    {
        foreach (Type entry in _deckService.CardDeckSave)
        {
            DeckEntry deckEntry = _deckEntryFactory.GetObject();
            Core.Towers.AbstractTower tower = _abstractTowerPrefabContainer.TowerDictionary[entry];
            deckEntry.InitContent(tower, tower.GetType());
        }
    }
    public void DeckEntryPressed(DeckEntry entry)
    {
        _deckService.CardDeckSave.Remove(entry.RelatedTowerType);

        CardEntry card = _cardEntryFactory.GetObject();
        card.InitContent(_abstractTowerPrefabContainer.TowerDictionary[entry.RelatedTowerType], entry.RelatedTowerType);
        card.AwaitForRelease();
    }

    public bool TryInsertToDeck(CardEntry entry)
    {
        if (!IsInDeckZone(entry.transform.position)) //if we release card outside of deck zone
            return false;
        else //if we release card in deck zone
        {
            _deckService.CardDeckSave.Add(entry.RelatedTowerType);

            DeckEntry deckEntry = _deckEntryFactory.GetObject();
            deckEntry.InitContent(_abstractTowerPrefabContainer.TowerDictionary[entry.RelatedTowerType], entry.RelatedTowerType);

            return true;
        }
    }
    private bool IsInDeckZone(Vector3 localPos) =>
        Screen.width * _deckBuildingUiContainer.DeckAreaTransform.anchorMin.x < localPos.x;
}
