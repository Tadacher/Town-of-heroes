using Core.Towers;
using Infrastructure;
using System;
using System.Collections.Generic;

namespace Services.CardGeneration
{
    public class CardDeckService
    {
        public List<Type> CardDeck => _cardDeck;

        private List<Type> _cardDeck;
        private CardDeckSaveLoader _cardDeckLoaderService;

        public CardDeckService(CardDeckSaveLoader cardDeckLoaderService)
        {
            _cardDeckLoaderService = cardDeckLoaderService;
            _cardDeck = _cardDeckLoaderService.CardDeckSave;
        }

        public Type DraftRandom() =>
            GetRandomCardType();
        private Type GetRandomCardType() =>
            _cardDeck[UnityEngine.Random.Range(0, _cardDeck.Count)];
    }
}