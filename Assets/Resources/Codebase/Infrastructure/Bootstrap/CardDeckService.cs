using Core.Towers;
using System;

namespace Services.CardGeneration
{
    public class CardDeckService
    {
        private CardInstantiationService _cardInstantiationService;
        private Type[] _cardDeck;
        public CardDeckService(CardInstantiationService cardInstantiationService)
        {
            _cardInstantiationService = cardInstantiationService;
            _cardDeck = new Type[]
            {
                typeof(ArcherTower),
                typeof(TowerOfDeath),
            };
        }

        public void DraftRandom(int count)
        {
            for (int i = 0; i < count; i++)
                _cardInstantiationService.ReturnObject(GetRandomCardType());
        }
        public void DraftRandom() =>
            _cardInstantiationService.ReturnObject(GetRandomCardType());

        private Type GetRandomCardType() =>
            _cardDeck[UnityEngine.Random.Range(0, _cardDeck.Length)];
    }
}