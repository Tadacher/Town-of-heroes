using Services.Factories;
using Services.Input;
using UnityEngine;
using Zenject;

namespace Metagameplay.Buildings
{
    internal class MetaGridCellFactory : AbstractPoolerFactory<AbstractMetaGridCell>
    {
        private readonly AbstractMetaGridCell _prefab;
        private readonly AbstractInputService _input;
        private readonly MetaGridSevice _metaGridService;
        private readonly MetaCityService _metaCityService;

        public MetaGridCellFactory(
                            AbstractMetaGridCell abstractMetaGridCell,
                            DiContainer container,
                            AbstractInputService input,
                            MetaGridSevice metaGridService,
                            MetaCityService metaCityService) : base(container)
        {
            _prefab = abstractMetaGridCell;
            _input = input;
            _metaGridService = metaGridService;
            _metaCityService = metaCityService;
        }

        protected override void ActionOnDestroy(AbstractMetaGridCell poolable) =>
            Object.Destroy(poolable.gameObject);

        protected override void ActionOnGet(AbstractMetaGridCell poolable) =>
            poolable.gameObject.SetActive(true);

        protected override void ActionOnRelease(AbstractMetaGridCell returnable) =>
            returnable.gameObject.SetActive(false);

        protected override AbstractMetaGridCell CreateNew()
        {
            var product = Object.Instantiate(_prefab, null);
            product.Initialize(abstractInputService: _input,
                               abstractGridService: _metaGridService,
                               metaCityService: _metaCityService,
                               objectPooler: this);
            return product;
        }
    }
}