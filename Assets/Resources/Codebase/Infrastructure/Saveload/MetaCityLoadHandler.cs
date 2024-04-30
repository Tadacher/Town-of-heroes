using Infrastructure;
using Metagameplay.Buildings;
using Progress;
using UnityEngine;

/// <summary>
/// loading, deserialization and applying of meta city save
/// </summary>
public class MetaCityLoadHandler
{
    private MetaGridSevice _metaGridSevice;
    private MetaBuildingsInstantiationService _buildingsInstantiationService;
    private MetaCitySaveLoader _saveLoader;
    private readonly MetaCityService _metaCityService;
    private readonly ResourceService _resourceService;

    public MetaCityLoadHandler(MetaGridSevice metaGridSevice,
                               MetaBuildingsInstantiationService buildingsInstantiationService,
                               MetaCitySaveLoader saveLoader,
                               MetaCityService metaCityService,
                               ResourceService resourceService)
    {
        _metaGridSevice = metaGridSevice;
        _buildingsInstantiationService = buildingsInstantiationService;
        _saveLoader = saveLoader;
        _metaCityService = metaCityService;
        _resourceService = resourceService;
        MetaCitySave save = _saveLoader.SaveObject;
        if (save == null || save.FlatGrid == null)
        {
            Debug.LogError("No save found!");
            return;
        }
        else
            LoadInjectedBuildings(save);
    }

    private void LoadInjectedBuildings(MetaCitySave save)
    {
        InsertBuildingsFromSave(save);
        _metaCityService.ApplyEffects();
        ApplyWaveEffects();
    }

    private void ApplyWaveEffects()
    {
        _metaCityService.ApplyWaveEffects(_resourceService.GetResourceData().Waves);
        _resourceService.ClearWave();
        _resourceService.Save();
    }

    private void InsertBuildingsFromSave(MetaCitySave save)
    {
        for (int x = 0; x < _metaGridSevice.SizeX; x++)
        {
            for (int y = 0; y < _metaGridSevice.SizeY; y++)
            {
                int flatIndex = _metaGridSevice.SizeX * x + y;
                if (save.FlatGrid[flatIndex] != null)
                {
                    AbstractMetaGridCell cell = _buildingsInstantiationService.ReturnObject(save.FlatGrid[flatIndex].BuildingType);
                    cell.SetLevel(save.FlatGrid[flatIndex].BuildingLevel);
                    cell.transform.position = _metaGridSevice.CelCoordsToPos(new Vector2(x, y));
                    cell.InsertSelfToGrid();
                }
            }
        }
    }
}
