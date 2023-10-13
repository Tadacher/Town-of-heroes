using Core.Towers;
using System;

namespace Services.TowerBuilding
{
    public class TowerInstantiationService : AbstractInstantiationService<AbstractTower>
    {
        public override TReturnable ReturnObject<TReturnable>()
        {
            if (!_factories.ContainsKey(typeof(TReturnable)))
            {
                AddNewFactory<TReturnable>();
            }
            return ((IFactory<TReturnable>)_factories[typeof(TReturnable)]).GetObject();
        }

        private IFactory<TReturnable> AddNewFactory<TReturnable>() where TReturnable : AbstractTower => 
            new TowerFactory<TReturnable>();
    }
}