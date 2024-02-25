namespace Services.CardGeneration
{
    /// <summary>
    /// General service for generating card in players hands
    /// </summary>
    public class CardGenerationService
    {
        private CardDeckService _cardDeckService;
        private CardInstantiationService _cardInstantiationService;

        public CardGenerationService(CardDeckService cardDeckService)
        {
            _cardDeckService = cardDeckService;
        }

        public void GenerateInitialCards() => _cardDeckService.DraftRandom(3);
        public void DraftCard() => _cardDeckService.DraftRandom();
    }
}