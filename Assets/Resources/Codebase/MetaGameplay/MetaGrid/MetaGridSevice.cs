using Core.Towers;
using Metagameplay.Buildings;
using Services.GridSystem;
using System;
using UnityEngine;
using static Infrastructure.MetaCitySave;

/// <summary>
/// Meta grid and grid-level interaction logic
/// </summary>
public class MetaGridSevice : AbstractGridService
{
    public int SizeX => _gridSizeX;
    public int SizeY => _gridSizeY; 

    private const float _cellSize = 2.05f;
    private const int _gridSize_X = 20;
    private const int _gridSize_Y = 20;

    public MetaGridSevice() : base(Vector2.one/2, Vector2.zero, _cellSize, _gridSize_X, _gridSize_Y)
    {
    }
    public MetaCitySaveEntry[] GetSaveData()
    {
        MetaCitySaveEntry[] types = new MetaCitySaveEntry[_gridSizeX*_gridSizeY];

        for (int x = 0; x < _gridSizeX; x++)
        {
            for(int y = 0; y < _gridSizeY; y++)
            {
                int flatIndex = _gridSizeX * x + y;

                if (_cells[x, y] == null)
                    types[flatIndex] = null;

                else
                {
                    var cell = (_cells[x, y] as AbstractMetaGridCell);

                    if (cell.ShouldNotSave)
                        continue;
                    
                    Type buildingType = _cells[x, y].GetType();
                    int level = cell.Level;
                    types[flatIndex] = new(buildingType, level);
                }
            }
        }
        return types;
    }
    protected override void InitCellContent()
    {
        
    }
}
