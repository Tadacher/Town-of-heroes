using Infrastructure;
using Services.Factories;
using Services.Input;
using Services.TowerBuilding;
using Services.Ui;
using System;
using UnityEngine;
using WorldCellBuilding.CardImage;
using Zenject;

namespace Services.CardGeneration
{
    public class TowerCardFactory : AbstractPoolerFactory<TowerCard>, IFactory<TowerCard>
    {
        [SerializeField] private CellStats _cellStats;
        private readonly CardImageDatabase _cardImageDatabase;
        private readonly TowerImageDatabase _towerImageDatabase;
        private readonly WorldCellCardGenerator _worldCellCardGenerator;
        private readonly CardDescriptionService _cardDescriptionService;
        private readonly TowerCard _cardPrefab;
        private Type _towerType;

        public TowerCardFactory(Type type,
                                TowerCard cardPrefab,
                                CardImageDatabase cardImageDatabase,
                                WorldCellCardGenerator worldCellCardGenerator,
                                CardDescriptionService towerCardDescriptionService,
                                DiContainer diContainer,
                                TowerImageDatabase towerImageDatabase) : base(diContainer)
        {
            _cardDescriptionService = towerCardDescriptionService;
            _cardImageDatabase = cardImageDatabase;
            _cardImageDatabase.Initialize();
            _worldCellCardGenerator = worldCellCardGenerator;
            _towerType = type;
            _cardPrefab = cardPrefab;
            _towerImageDatabase = towerImageDatabase;
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

        protected override void ActionOnRelease(TowerCard returnable) => 
            returnable.gameObject.SetActive(false);

        protected override TowerCard CreateNew()
        {
            TowerCard returnable = GameObject.Instantiate(_cardPrefab, null);

            Type worldCellType = GetWorldCellType();
            try
            {
                returnable.Initialize(
                    towerBuildingService: _container.Resolve<TowerBuildingService>(),
                    worldCellBuildingService: _container.Resolve<WorldCellBuildingService>(),
                    gameplayStateMachine: _container.Resolve<GameplayStateMachine>(),
                    shiftEventProvider: _container.Resolve<AbstractInputService>(),
                    cardInfoUiService: _container.Resolve<CardInfoUiService>(),
                    inputService: _container.Resolve<AbstractInputService>(),
                    towerCardInfoConfig: _cardDescriptionService.LoadCardDescription(_towerType),
                    worldCellCardInfoConfig: _cardDescriptionService.LoadWorldCellCardDescription(worldCellType),
                    worldCellSprite: _cardImageDatabase.GetSprite(worldCellType.Name),
                    towerSprite: _towerImageDatabase.GetSprite(_towerType.Name),
                    pooler: this,
                    towerType: _towerType,
                    worldCellType: worldCellType);
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
            return returnable;
        }

        private Type GetWorldCellType() => _worldCellCardGenerator.GetWorldCellType(_towerType);
    }
}