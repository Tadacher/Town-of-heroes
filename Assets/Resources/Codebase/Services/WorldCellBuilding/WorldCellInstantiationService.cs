using Services.Factories;
using System;
using WorldCells;
using Zenject;

/// <summary>
/// General service for instantiating all types of world cells
/// </summary>
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