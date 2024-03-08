using Services.Input;
using System;

namespace Metagameplay.Buildings
{
    public class MetaBuildingsInstantiationService : AbstractInstantiationService<AbstractMetaGridCell>
    {
        private const string _prefabPath = "Prefabs/CityBuildings/";
        public MetaBuildingsInstantiationService(Zenject.DiContainer diContainer) : base(_prefabPath, diContainer)
        {

        }

        public override AbstractMetaGridCell ReturnObject(Type type)
        {
            if (!_factories.ContainsKey(type))
            {
                AddNewFactory(type);
            }
            var returnable = _factories[type].GetObject();
            return returnable;
        }

        protected override void AddNewFactory(Type type) =>
            _factories.Add(type, GetNewFactory(type));

        protected override IFactory<AbstractMetaGridCell> GetNewFactory(Type productType)
        {
            return new MetaGridCellFactory(abstractMetaGridCell: LoadProductPrefab(productType),
                                           metaGridService: _container.Resolve<MetaGridSevice>(),
                                           metaCityService: _container.Resolve<MetaCityService>(),
                                           input: _container.Resolve<AbstractInputService>(),
                                           container: _container);
        }
    }
}