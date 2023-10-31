using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathfindingAlgorhytm : MonoBehaviour
{
    [SerializeField]
    Tilemap tilemap;
    [SerializeField]
    Tile baseCamp;
    [SerializeField]
    Grid grid;
    [SerializeField]
    Vector2Int spawnerPosition = new Vector2Int(6, 6), baseposition = new Vector2Int(0, 0);
    private void Start()
    {
        FindPath();
        
       
        
    }
    private void Update()
    {
        //Debug.Log(tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition)));
       // if (tilemap.GetTile<Tile>(tilemap.WorldToCell(Camera.main.ScreenToWorldPoint(Input.mousePosition))) == baseCamp) Debug.Log("aye");
        //else Debug.Log("no shit");
    }
    void FindPath()
    {
        //tilemap.layoutGrid.
        var bounds = tilemap.size;
        Vector2[,] cells = new Vector2[bounds.x, bounds.y];

        for (int lenght = 0; lenght < bounds.x; lenght++)
        {
            for(int height = 0; height < bounds.y; height++)
            {
                cells[lenght, height] = new Vector2(lenght, height);
              //  Debug.Log(cells[lenght, height]);
            }
        }
    }
}
