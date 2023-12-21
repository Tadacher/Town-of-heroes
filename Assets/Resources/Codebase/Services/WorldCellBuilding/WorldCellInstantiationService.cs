using Services.Factories;
using Services.GridSystem;
using Services.Input;
using System;

public class WorldCellInstantiationService : AbstractInstantiationService<AbstractWorldCell>
{
    private const string _prefabPath = "Prefabs/WorldCells/";
    private AbstractInputService _inputInputService;
    private BattleGridService _alignService;

    public WorldCellInstantiationService(AbstractInputService inputInputService, BattleGridService alignService) : base(_prefabPath)
    {
        _inputInputService = inputInputService;
        _alignService = alignService;
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
        new WorldCellFactory(LoadProductPrefab(productType));
}