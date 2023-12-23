using Services.Factories;
using Services.GlobalMap;
using Services.GridSystem;
using Services.Input;
using System;
using WorldCells;

public class WorldCellInstantiationService : AbstractInstantiationService<AbstractWorldCell>
{
    private const string _prefabPath = "Prefabs/WorldCells/";
    private readonly WorldCellGridService _worldCellGridService;
    private readonly AbstractInputService _abstractInputService;
    private readonly WorldCellBalanceService _worldCellBalanceService;

    public WorldCellInstantiationService( WorldCellGridService alignService,
                                         AbstractInputService abstractInputService,
                                         WorldCellBalanceService worldCellBalanceService) : base(_prefabPath)
    {
        _worldCellGridService = alignService;
        _abstractInputService = abstractInputService;
        _worldCellBalanceService = worldCellBalanceService;
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
        new WorldCellFactory(LoadProductPrefab(productType), _worldCellGridService, _abstractInputService, _worldCellBalanceService);
}