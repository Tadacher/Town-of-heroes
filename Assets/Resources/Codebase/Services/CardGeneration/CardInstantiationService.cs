using System;
using Codebase.MonobehaviourComponents;
using UnityEngine;
using WorldCellBuilding.CardImage;
using Zenject;

namespace Services.CardGeneration
{
    public class CardInstantiationService : AbstractInstantiationService<TowerCard>
    {
        private const string _prefabpath = "Prefabs/Cards/";
        private Transform _cardParent;

        public CardInstantiationService(
            DiContainer diContainer,
            TowerCardSpawnMarker towerCardSpawnMarker) : base(_prefabpath, diContainer)
            => _cardParent = towerCardSpawnMarker.transform;

        public override TowerCard ReturnObject(Type type)
        {
            if (!_factories.ContainsKey(type))
                AddNewFactory(type);

            var card = _factories[type].GetObject();
            card.transform.SetParent(_cardParent);

            return card;
        }

        protected override void AddNewFactory(Type type) =>
            _factories.Add(type, GetNewFactory(type));

        protected override IFactory<TowerCard> GetNewFactory(Type type) =>
            new TowerCardFactory(
                type: type,
                cardPrefab: LoadProductPrefab(type),
                towerCardDescriptionService: _container.Resolve<CardDescriptionService>(),
                cardImageDatabase: _container.Resolve<CardImageDatabase>(),
                worldCellCardGenerator: _container.Resolve<WorldCellCardGenerator>(),
                towerImageDatabase: _container.Resolve<TowerImageDatabase>(),
                diContainer: _container);
    }
}
