using Core.Towers;
using Services.GridSystem;
using Services.Input;
using System;

namespace Services.TowerBuilding
{
    public class TowerInstantiationService : AbstractInstantiationService<AbstractTower>
    {
        private const string _prefabPath = "Prefabs/Towers/";
        private AbstractInputService _inputInputService;
        private GridAlignService _alignService;

        public TowerInstantiationService(AbstractInputService abstractInputService, GridAlignService gridAlignService) : base(_prefabPath)
        {
            _alignService = gridAlignService;
            _inputInputService = abstractInputService;
        }

        public override AbstractTower ReturnObject(Type type)
        {
            if (!_factories.ContainsKey(type))
                AddNewFactory(type);

            return _factories[type].GetObject();
        }

        protected override void AddNewFactory(Type type) => 
            _factories.Add(type, GetNewFactory(type));

        protected override IFactory<AbstractTower> GetNewFactory(Type type) => 
            new TowerFactory(LoadProductPrefab(type), _inputInputService, _alignService);
    }
}