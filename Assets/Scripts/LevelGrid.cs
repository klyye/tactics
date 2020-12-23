public class LevelGrid
{
    private readonly Location[,] _grid;

    public LevelGrid(int w, int h)
    {
        _grid = new Location[w, h];

        // grid starts out as all ground with no units
        for (var r = 0; r < _grid.GetLength(1); r++)
        for (var c = 0; c < _grid.GetLength(0); c++)
            _grid[c, r] = new Location(null, Terrain.GROUND);
    }

    public int width => _grid.GetLength(0);

    public int height => _grid.GetLength(1);

    public Location GetLocation(Coord c)
    {
        return _grid[c.x, c.y];
    }

    public void SetTerrain(Coord c, Terrain t)
    {
        _grid[c.x, c.y].terrain = t;
    }

    public void PlaceUnit(Coord c, Unit u)
    {
        _grid[c.x, c.y].unit = u;
    }

    public bool WithinBounds(Coord c)
    {
        return c.x >= 0 && c.y >= 0 && c.x < width && c.y < height;
    }
}