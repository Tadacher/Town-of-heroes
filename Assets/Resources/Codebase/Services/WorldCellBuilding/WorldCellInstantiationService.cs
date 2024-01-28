using Services.Factories;
using Services.GlobalMap;
using Services.GridSystem;
using Services.Input;
using System;
using WorldCells;
using Zenject;

public class WorldCellInstantiationService : AbstractInstantiationService<AbstractWorldCell>
{
    private const string _prefabPath = "Prefabs/WorldCells/";
    public WorldCellInstantiationService(DiContainer diContainer) : base(_prefabPath, diContainer)
    {

    }
    public override AbstractWorldCell ReturnObject(Type type)
    {
        if (!_factories.ContainsKey(type))
            AddNewFactory(type);

        return _factories[type].GetObject();
    }

    protected override void AddNewFactory(Type type) => 
        _factories.Add(type, GetNewFactory(type));

    protected override IFactory<AbstractWorldCell> GetNewFactory(Type productType) => 
        new WorldCellFactory(LoadProductPrefab(productType), _container);
}