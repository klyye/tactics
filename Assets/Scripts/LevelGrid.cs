using UnityEngine;

/// <summary>
///     Contains data about the current shape and land layout of the level.
/// </summary>
public class LevelGrid
{
    private readonly Land[,] _grid;

    public LevelGrid(int w, int h)
    {
        _grid = new Land[w, h];

        // grid starts out as all ground with no units
        for (var r = 0; r < _grid.GetLength(1); r++)
        for (var c = 0; c < _grid.GetLength(0); c++)
            _grid[c, r] = Land.DIRT;
    }

    public int width => _grid.GetLength(0);

    public int height => _grid.GetLength(1);

    public Land GetLand(Vector2Int c)
    {
        return _grid[c.x, c.y];
    }

    public void SetTerrain(Vector2Int c, Land t)
    {
        _grid[c.x, c.y] = t;
    }


    public bool WithinBounds(Vector2Int c)
    {
        return c.x >= 0 && c.y >= 0 && c.x < width && c.y < height;
    }
}