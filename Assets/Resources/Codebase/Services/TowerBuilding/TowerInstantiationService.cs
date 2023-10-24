using Core.Towers;
using System;

namespace Services.TowerBuilding
{
    public class TowerInstantiationService : AbstractInstantiationService<AbstractTower>
    {
        public TowerInstantiationService(string productionPrefabPath) : base(productionPrefabPath)
        {
        }

        public override AbstractTower ReturnObject(Type type)
        {
            if (!_factories.ContainsKey(type))
                AddNewFactory(type);

            return _factories[type].GetObject();
        }

        protected override void AddNewFactory(Type type)
        {
            _factories.Add(type, GetNewFactory(type));
        }

        protected override IFactory<AbstractTower> GetNewFactory(Type type)
        {
            return new TowerFactory();
        }
    }
}