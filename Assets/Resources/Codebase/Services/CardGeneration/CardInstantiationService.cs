using System;
using Codebase.MonobehaviourComponents;
using Core.Towers;
using UnityEngine;
using WorldCellBuilding.CardImage;
using Zenject;

namespace Services.CardGeneration
{
    public class CardInstantiationService : AbstractInstantiationService<TowerCard>
    {
        private const string _prefabpath = "Prefabs/Cards/";
        private readonly CardCountService _cardCountService;
        private Transform _cardParent;
        private Transform _commonCanvas;

        public CardInstantiationService(
            DiContainer diContainer,
            TowerCardSpawnMarker towerCardSpawnMarker,
            CommonCanvasMarker commonCanvasMarker,
            CardCountService cardCountService) : base(_prefabpath, diContainer)
        {
            _commonCanvas = commonCanvasMarker.transform;
            _cardParent = towerCardSpawnMarker.transform;
            _cardCountService = cardCountService;
        }

        public override TowerCard ReturnObject(Type type)
        {
            if (!_factories.ContainsKey(type))
                AddNewFactory(type);

            _cardCountService.NotifyAboutNewCard();
            TowerCard card = _factories[type].GetObject();
            card.StartTranslationCoroutine(_cardParent);
            card.transform.SetParent(_commonCanvas);
            return card;
        }

        protected override void AddNewFactory(Type type) =>
            _factories.Add(type, GetNewFactory(type));

        protected override IFactory<TowerCard> GetNewFactory(Type type) =>
            new TowerCardFactory(
                type: type,
                cardPrefab: LoadProductPrefab(typeof(ArcherTower)),
                towerCardDescriptionService: _container.Resolve<CardDescriptionService>(),
                cardImageDatabase: _container.Resolve<CardImageDatabase>(),
                worldCellCardGenerator: _container.Resolve<WorldCellCardGenerator>(),
                towerImageDatabase: _container.Resolve<TowerImageDatabase>(),
                diContainer: _container);
    }
}
