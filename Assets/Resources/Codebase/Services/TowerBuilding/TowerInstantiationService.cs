using Core.Towers;
using Services.Input;
using System;

namespace Services.TowerBuilding
{
    public class TowerInstantiationService : AbstractInstantiationService<AbstractTower>
    {
        private const string _prefabPath = "Prefabs/Towers/";
        AbstractInputService _inputInputService;
        public TowerInstantiationService(AbstractInputService abstractInputService) : base(_prefabPath)
        {
            _inputInputService = abstractInputService;
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
            return new TowerFactory(LoadProductPrefab(type), _inputInputService);
        }
    }
}