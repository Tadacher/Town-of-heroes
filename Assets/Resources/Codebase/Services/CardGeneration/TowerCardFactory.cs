using Core.Towers;
using Services.TowerBuilding;
using System;
using UnityEngine;

namespace Services.CardGeneration
{
    public class TowerCardFactory : AbstractPoolerFactory<TowerCard>, IFactory<TowerCard>
    {
        private readonly TowerBuildingService _towerBuildingService;
        private readonly TowerCard _cardPrefab;
        private Type _type;

        public TowerCardFactory(Type type, TowerCard cardPrefab, TowerBuildingService towerBuildingService) : base()
        {
            _type = type;
            Debug.Log(cardPrefab);
            _cardPrefab = cardPrefab;
            _towerBuildingService = towerBuildingService;
        }

        public override TowerCard GetObject() =>
            _pool.Get();

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
            returnable.Initialize(_towerBuildingService, _type);
            return returnable;
        }
    }
}