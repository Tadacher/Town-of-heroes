using Services.GridSystem;
using System;
using UnityEngine;

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
    public Type[] GetSaveData()
    {
        Type[] types = new Type[_gridSizeX*_gridSizeY];

        for (int x = 0; x < _gridSizeX; x++)
        {
            for(int y = 0; y < _gridSizeY; y++)
            {
                int flatIndex = _gridSizeX * x + y;

                if (_cells[x, y] == null)
                    types[flatIndex] = null;

                else
                    types[flatIndex] = _cells[x, y].GetType();
            }
        }
        return types;
    }
    protected override void InitCellContent()
    {
        
    }
}
