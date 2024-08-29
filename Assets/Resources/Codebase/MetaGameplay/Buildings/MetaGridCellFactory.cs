using Services.Factories;
using Services.Input;
using UnityEngine;
using Zenject;

namespace Metagameplay.Buildings
{
    internal class MetaGridCellFactory : MonobehaviourAbstractPoolerFactory<AbstractMetaGridCell>
    {
        private readonly AbstractMetaGridCell _prefab;
        private readonly AbstractInputService _input;
        private readonly MetaGridSevice _metaGridService;
        private readonly MetaCityInfoService _metaCityService;

        public MetaGridCellFactory(
                            AbstractMetaGridCell abstractMetaGridCell,
                            DiContainer container,
                            AbstractInputService input,
                            MetaGridSevice metaGridService,
                            MetaCityInfoService metaCityService) : base(container)
        {
            _prefab = abstractMetaGridCell;
            _input = input;
            _metaGridService = metaGridService;
            _metaCityService = metaCityService;
        }

        protected override AbstractMetaGridCell CreateNew()
        {
            AbstractMetaGridCell product = _container.InstantiatePrefabForComponent<AbstractMetaGridCell>( prefab: _prefab, parentTransform: null);
            product.InjectDependencies(this);
            return product;
        }
    }
}