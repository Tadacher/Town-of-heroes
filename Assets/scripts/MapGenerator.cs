using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    int[,] tiles;
    int iterations, maxIterations = 50;
    const int Xsize = 10, Ysize = 10;
    [SerializeField] Tilemap tilemap;
    [SerializeField]Tile basicTile;
    [SerializeField]Tile playerBase;
    [SerializeField]Tile enemyBase;
    [SerializeField] Tile road;
    List<Vector3Int> roadmap = new List<Vector3Int>();

    static Vector2Int basePlacce = new Vector2Int(0,0);
    Vector2Int _walkerpos;
    Vector3Int Walkerpos
    {
        get
        {
            return new Vector3Int(_walkerpos.x, _walkerpos.y, 0);
        }
        set 
        {
            if (value.x > Xsize - 1) Debug.Log("Walkerpos x is outta range");
            else _walkerpos.x = value.x;
            if (value.y > Ysize - 1) Debug.Log("Walkerpos Y is outta range");
            else _walkerpos.y = value.y;

        }
    }
    private void Start()
    {
        tiles = new int[Xsize, Ysize];
        Mapgen();
        WalkerGen();
    }
    void Mapgen()
    {
        
        for (int lenght = 0; lenght<Xsize;  lenght++)
        {
            for(int height = 0; height<Ysize; height++)
            {
                tilemap.SetTile(new Vector3Int(lenght, height, 0), basicTile);
            }
        }
        tilemap.SetTile(new Vector3Int(0, 0, 0), playerBase);
        tilemap.SetTile(new Vector3Int(Xsize-1, Ysize-1, 0), enemyBase);

    }
    void WalkerGen()
    {
        Walkerpos = new(Xsize - 1, Ysize - 1);
        while(iterations<maxIterations)
        {
            Walk();
        }
    }
    void Walk()
    {
        if (CheckIfCanWalk()) iterations++;
        else iterations = maxIterations;

        if (iterations >= maxIterations) return;
        Vector2Int direction = new();
        //random direction world sided, clock-wised
        switch (Random.Range(0, 4))
        {
            case 0:
               direction = new(0, 1);
                    break;
            case 1:
                direction = new(1, 0);
                break;
            case 2:
                direction = new(0, -1);
                break;
            case 3:
                direction = new(-1, 0);
                break;
        }
        //Check if it is goodPlaceToRoad
        Vector3Int newpos = new(Walkerpos.x + direction.x, Walkerpos.y + direction.y, 0);
        if (CheckIfTileWalkable(newpos))
        {
            roadmap.Add(newpos);
            tilemap.SetTile(newpos, road);
            Walkerpos = new Vector3Int(newpos.x, newpos.y, 0);
        }
        else Walk();
    }
    bool CheckIfTileWalkable(Vector3Int where)
    {
        Tile tile = tilemap.GetTile<Tile>(where);
        if (tile == road 
            || tile == enemyBase
            || where.x > Xsize - 1 
            || where.y > Ysize - 1)
            return false;
        else return true;
    }
    bool CheckIfCanWalk()
    {
        Tile tile = tilemap.GetTile<Tile>(Walkerpos);
        if (
            CheckIfTileWalkable(Walkerpos + new Vector3Int(1, 0, 0))
          && CheckIfTileWalkable(Walkerpos + new Vector3Int(-1, 0, 0))
          && CheckIfTileWalkable(Walkerpos + new Vector3Int(0, 1, 0))
          && CheckIfTileWalkable(Walkerpos + new Vector3Int(0, -1, 0)))
            return false;
        else return true;
    }
}
