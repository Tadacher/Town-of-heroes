namespace Services.CardGeneration
{
    /// <summary>
    /// General service for generating card in players hands
    /// </summary>
    public class CardGenerationService
    {
        private CardDeckService _cardDeckService;
        private CardInstantiationService _cardInstantiationService;

        public CardGenerationService(CardDeckService cardDeckService, CardInstantiationService cardInstantiationService)
        {
            _cardInstantiationService = cardInstantiationService;
            _cardDeckService = cardDeckService;
        }

        public void GenerateInitialCards()
        {
            for (int i = 0; i < 4; i++)
                DraftCard();
        }

        public void DraftCard()
        {
            var type = _cardDeckService.DraftRandom();
            _cardInstantiationService.ReturnObject(type);
        }
    }
}