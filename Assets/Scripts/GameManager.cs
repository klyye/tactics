using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var lg = new LevelGrid(5, 5);
        lg.SetTerrain(new Coord(1, 1), Terrain.MOUNTAIN);
        lg.SetTerrain(new Coord(1, 2), Terrain.MOUNTAIN);
        lg.SetTerrain(new Coord(1, 3), Terrain.MOUNTAIN);
        lg.SetTerrain(new Coord(1, 4), Terrain.MOUNTAIN);
        
        lg.SetTerrain(new Coord(3, 0), Terrain.MOUNTAIN);
        lg.SetTerrain(new Coord(3, 1), Terrain.MOUNTAIN);
        lg.SetTerrain(new Coord(3, 2), Terrain.MOUNTAIN);
        lg.SetTerrain(new Coord(3, 3), Terrain.MOUNTAIN);
        var astar = new AStarSolver(lg, Coord.Dist);
        var path = astar.ShortestPath(new Coord(0, 4), new Coord(4, 0));
        foreach (var c in path)
        {
            Debug.Log(c);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
