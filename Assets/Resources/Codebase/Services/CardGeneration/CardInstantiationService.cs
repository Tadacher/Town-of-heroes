using System;
using Codebase.MonobehaviourComponents;
using Infrastructure;
using Services.CardGeneration;
using Services.TowerBuilding;
using UnityEngine;

namespace Codebase.Services.CardGeneration
{
    public class CardInstantiationService : AbstractInstantiationService<TowerCard>
    {
        private const string _prefabpath = "Prefabs/Cards/"; 
        private Transform _cardParent;

        private readonly WorldCellBuildingService _worldCellBuildingService;
        private readonly TowerBuildingService _towerBuildingService;
        private readonly GameplayStateMachine _gameplayStateMachine;
        public CardInstantiationService(TowerBuildingService towerBuildingService,
                                        TowerCardSpawnMarker cardparent,
                                        WorldCellBuildingService worldCellBuildingService,
                                        GameplayStateMachine gameplayStateMachine) : base(_prefabpath)
        {
            _gameplayStateMachine = gameplayStateMachine;
            _worldCellBuildingService = worldCellBuildingService;
            _towerBuildingService = towerBuildingService;
            _cardParent = cardparent.transform;
        }

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
                worldCellBuildingService: _worldCellBuildingService,
                towerBuildingService: _towerBuildingService,
                gameplayStateMachine: _gameplayStateMachine);
    }
}
