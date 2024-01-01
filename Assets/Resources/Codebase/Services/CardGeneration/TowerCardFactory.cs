using Core.Towers;
using Infrastructure;
using Services.Factories;
using Services.TowerBuilding;
using System;
using UnityEngine;
using WorldCells;

namespace Services.CardGeneration
{
    public class TowerCardFactory : AbstractPoolerFactory<TowerCard>, IFactory<TowerCard>
    {
        [SerializeField] private CellStats _cellStats;
        private readonly GameplayStateMachine _gameplayStateMachine;
        private readonly TowerBuildingService _towerBuildingService;
        private readonly WorldCellBuildingService _worldCellBuildingService;
        private readonly TowerCard _cardPrefab;
        private readonly WorldCellCardGenerator _worldCellCardGenerator;
        private Type _towerType;

        public TowerCardFactory(Type type,
                                TowerCard cardPrefab,
                                WorldCellBuildingService worldCellBuildingService,
                                TowerBuildingService towerBuildingService,
                                GameplayStateMachine gameplayStateMachine,
                                WorldCellCardGenerator worldCellCardGenerator) : base()
        {
            _worldCellBuildingService = worldCellBuildingService;
            _gameplayStateMachine = gameplayStateMachine;
            _towerType = type;
            _cardPrefab = cardPrefab;
            _towerBuildingService = towerBuildingService;
            _worldCellCardGenerator = worldCellCardGenerator;
        }

        public override TowerCard GetObject()
        {
            TowerCard gettable = _pool.Get();
            return gettable.ReInitialize();
        }

        public override void ReturnToPool(IPoolableObject poolable) =>
            _pool.Release((TowerCard)poolable);

        protected override void ActionOnDestroy(TowerCard poolable) => 
            GameObject.Destroy(poolable.gameObject);

        protected override void ActionOnGet(TowerCard poolable) =>
            poolable.gameObject.SetActive(true);

        protected override void ActionOnRelease(TowerCard returnable)
        {
            returnable.gameObject.SetActive(false);
        }

        protected override TowerCard CreateNew()
        {
            TowerCard returnable = GameObject.Instantiate(_cardPrefab, null);
            returnable.Initialize(
                towerBuildingService: _towerBuildingService, 
                worldCellBuildingService: _worldCellBuildingService,
                gameplayStateMachine: _gameplayStateMachine, 
                pooler: this, 
                towerType: _towerType, 
                worldCellType: GetWorldCellType());
            return returnable;
        }

        private Type GetWorldCellType() => _worldCellCardGenerator.GetWorldCellType(_towerType);
    }
}