using Infrastructure;
using Metagameplay.Buildings;
using UnityEngine;

public class MetaCityLoadHandler
{
    private MetaGridSevice _metaGridSevice;
    private MetaBuildingsInstantiationService _buildingsInstantiationService;
    private MetaCitySaveLoader _saveLoader;

    public MetaCityLoadHandler(MetaGridSevice metaGridSevice, MetaBuildingsInstantiationService buildingsInstantiationService, MetaCitySaveLoader saveLoader)
    {
        _metaGridSevice = metaGridSevice;
        _buildingsInstantiationService = buildingsInstantiationService;
        _saveLoader = saveLoader;

        MetaCitySave save = _saveLoader.SaveObject;
        if (save == null || save.FlatGrid == null)
        {
            Debug.LogError("No save found!");
            return;
        }

        for (int x = 0; x < _metaGridSevice.SizeX; x++)
        {
            for (int y = 0; y < _metaGridSevice.SizeY; y++) 
            {
                int flatIndex = _metaGridSevice.SizeX * x + y;
                if (save.FlatGrid[flatIndex] != null)
                {
                    var cell = _buildingsInstantiationService.ReturnObject(save.FlatGrid[flatIndex]);
                    cell.transform.position = _metaGridSevice.CelCoordsToPos(new UnityEngine.Vector2(x, y));
                    _metaGridSevice.Insert(cell, cell.transform.position);
                }
            }
        }
    }
}
