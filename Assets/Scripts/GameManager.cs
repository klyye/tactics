using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Camera cam;

    public static LevelGrid gameGrid;

    [SerializeField] private GridTilemap _tilemap;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        gameGrid = new LevelGrid(5, 5);
        gameGrid.SetTerrain(new Vector2Int(1, 1), Land.WATER);
        gameGrid.SetTerrain(new Vector2Int(1, 2), Land.WATER);
        gameGrid.SetTerrain(new Vector2Int(1, 3), Land.WATER);
        gameGrid.SetTerrain(new Vector2Int(1, 4), Land.WATER);

        gameGrid.SetTerrain(new Vector2Int(3, 0), Land.WATER);
        gameGrid.SetTerrain(new Vector2Int(3, 1), Land.WATER);
        gameGrid.SetTerrain(new Vector2Int(3, 2), Land.WATER);
        gameGrid.SetTerrain(new Vector2Int(3, 3), Land.WATER);
        var astar = new AStarSolver(gameGrid, Vector2Int.Distance);
        var path = astar.ShortestPath(new Vector2Int(0, 4), new Vector2Int(4, 0));
        foreach (var c in path)
        {
            Debug.Log(c);
        }

    }

    // Update is called once per frame
    void Update()
    {
        _tilemap.UpdateGrid(gameGrid);
    }
}