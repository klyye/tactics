using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGrid
{
    private Location[,] _grid;

    public LevelGrid(int w, int h)
    {
        _grid = new Location[w, h];
        
        // grid starts out as all ground with no units
        for (int r = 0; r < _grid.GetLength(1); r++)
        {
            for (int c = 0; c < _grid.GetLength(0); c++)
            {
                _grid[c, r] = new Location(null, Feature.GROUND);
            }
        }
    }

    public void PlaceUnit(Coord c, Unit u)
    {
        _grid[c.x, c.y].unit = u;
    }
}