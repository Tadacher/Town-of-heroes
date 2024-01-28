using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MapGenerator : MonoBehaviour
{
    int[,] tiles;
    int iterations, maxIterations = 50;

    [SerializeField] float stdTurnChance;

    const int Xsize = 10, Ysize = 10;
    Vector2Int direction = new(0,0);
    
    
    [SerializeField]Tilemap tilemap;
    [SerializeField]Tile basicTile;
    [SerializeField]Tile playerBase;
    [SerializeField]Tile enemyBase;
    [SerializeField]Tile road;
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
    Vector3Int NextPos
    {
        get
        {
            return new(Walkerpos.x + direction.x, Walkerpos.y + direction.y, 0);
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
        else
        {
            Debug.Log("I cant walk!");
            iterations = maxIterations;
        }
        if (iterations >= maxIterations) return;

        if (direction == Vector2Int.zero || Random.Range(0f, 1f) <= stdTurnChance || !PosIsInsideBorders(NextPos)) direction = NewDirection();
        Debug.Log(direction);
        //Check if it is goodPlaceToRoad

        if (CheckIfPosWalkable(NextPos)) Step();
        else
        {
            CirculateDirection();
            if (CheckIfPosWalkable(NextPos)) Step();
            else
            {
                CirculateDirection();
                if (CheckIfPosWalkable(NextPos)) Step();
                else
                {
                    CirculateDirection();
                    if (CheckIfPosWalkable(NextPos)) Step();
                    else Debug.LogWarning("Circulate direction error: no available NextSteps found!");
                }
            }
        }
    }
    void CirculateDirection()
    {
        if (direction == Vector2Int.up) direction = Vector2Int.right;
        else if (direction == Vector2Int.right) direction = Vector2Int.down;
        else if (direction == Vector2Int.down) direction = Vector2Int.left;
        else if (direction == Vector2Int.left) direction = Vector2Int.up;
    }
    void Step()
    {
        roadmap.Add(NextPos);
        tilemap.SetTile(NextPos, road);
        Walkerpos = new Vector3Int(NextPos.x, NextPos.y, 0);
    }
    bool CheckIfPosWalkable(Vector3Int pos)
    {
        Tile tile = tilemap.GetTile<Tile>(pos);

        if (tile == road 
            || tile == enemyBase
            || tile == playerBase
            || !PosIsInsideBorders(pos))
            return false;
        else return true;
    }
    bool PosIsInsideBorders(Vector3Int pos)
    {
        if (pos.x > Xsize-1 || pos.x < 0) return false;
        else if(pos.y>Ysize-1 || pos.y<0) return false;
        else return true;
    }
    bool CheckIfCanWalk()
    {
        Tile tile = tilemap.GetTile<Tile>(Walkerpos);
        if (
            !CheckIfPosWalkable(Walkerpos + new Vector3Int(1, 0, 0))
          && !CheckIfPosWalkable(Walkerpos + new Vector3Int(-1, 0, 0))
          && !CheckIfPosWalkable(Walkerpos + new Vector3Int(0, 1, 0))
          && !CheckIfPosWalkable(Walkerpos + new Vector3Int(0, -1, 0)))
            return false;
        else return true;
    }
    Vector2Int NewDirection()
    {
        if (direction == Vector2Int.up || direction == Vector2Int.down)
        {
            if (Random.Range(0f, 1f) <= Walkerpos.x / Xsize) return Vector2Int.left;
            else return Vector2Int.right;
        }
        else if (direction == Vector2Int.left || direction == Vector2Int.right)
        {
            if (Random.Range(0f, 1f) <= Walkerpos.y / Ysize) return Vector2Int.down;
            else return Vector2Int.up;
        }
        else if (direction == Vector2.zero)
        {
            Debug.Log("Initial step");
            return Random.Range((int)0, 2) == 1 ? Vector2Int.left : Vector2Int.down;
        }
        else
        {
            Debug.Log("cant return new direction!");
            return Vector2Int.zero;
        }
    }
}
