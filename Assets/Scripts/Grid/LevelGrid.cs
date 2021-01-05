using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Rand = UnityEngine.Random;

/// <summary>
///     The grid that the game happens on...
/// </summary>
[RequireComponent(typeof(Tilemap))]
public class LevelGrid : MonoBehaviour
{
    [SerializeField] private int _width;
    [SerializeField] private int _height;

    /// <summary>
    ///     All possible terrains that can be used to generate the level grid.
    /// </summary>
    [SerializeField] private TerrainData[] _terrains;

    /// <summary>
    ///     A 2d array representing what terrain is at what (x, y) location in the grid.
    /// </summary>
    private TerrainData[,] _grid;

    /// <summary>
    ///     _occupied[x, y] is true iff there is something blocking movement through that square
    ///     (that is not terrain)
    /// </summary>
    private bool[,] _occupied;

    private Tilemap _tilemap;

    public int width => _width;
    public int height => _height;

    // Start is called before the first frame update
    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
        _grid = new TerrainData[width, height];
        _occupied = new bool[width, height];

        var walkables = _terrains.Where(l => l.walkable).ToArray();
        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
        {
            _occupied[x, y] = false;
            var roll = Rand.value;
            if (roll > 0.75)
                _grid[x, y] = walkables[Rand.Range(0, walkables.Length)];
            else
                _grid[x, y] = _terrains[Rand.Range(0, _terrains.Length)];
        }

        UpdateTilemap();
    }

    /// <summary>
    ///     Occupy the tile at coord, blocking movement through it.
    /// </summary>
    /// <param name="coord">The location to occupy.</param>
    public void Occupy(Vector2Int coord)
    {
        _occupied[coord.x, coord.y] = true;
    }

    /// <summary>
    ///     Unoccupy the tile at coord, allowing things to move through.
    /// </summary>
    /// <param name="coord">The location to unoccupy.</param>
    public void Unoccupy(Vector2Int coord)
    {
        _occupied[coord.x, coord.y] = false;
    }

    /// <summary>
    ///     Is the tile at coord occupied?
    /// </summary>
    /// <param name="coord">The location to check for occupation.</param>
    /// <returns>Whether or not the tile at coord is occupied.</returns>
    public bool IsOccupied(Vector2Int coord)
    {
        return _occupied[coord.x, coord.y];
    }

    /// <summary>
    ///     Update the unity tilemap to accurately the data in the TerrainData 2d array.
    /// </summary>
    private void UpdateTilemap()
    {
        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
        {
            var land = TerrainAt(x, y);
            var pos = new Vector3Int(x, y, 0);
            _tilemap.SetTile(pos, land.tile);
            _tilemap.SetTileFlags(pos, TileFlags.None);
        }
    }

    /// <summary>
    ///     Converts a pair of integer coordinates (x, y) to a Vector3 corresponding to the center
    ///     of the cell at those coordinates in world space.
    /// </summary>
    /// <param name="cellPosition">The grid coordinates of the cell to be converted.</param>
    /// <returns>The position of the center of that cell in world space.</returns>
    public Vector3 CoordToPosition(Vector2Int cellPosition)
    {
        return _tilemap.GetCellCenterWorld(cellPosition.ToVector3Int());
    }

    /// <summary>
    ///     Checks whether a coordinate (x, y) is within the bounds of the grid.
    /// </summary>
    /// <param name="coord">The coordinate to check.</param>
    /// <returns>Whether coord is within the bounds of the grid.</returns>
    public bool WithinBounds(Vector2Int coord)
    {
        return coord.x >= 0 && coord.y >= 0 && coord.x < width && coord.y < height;
    }

    /// <summary>
    ///     Returns the TerrainData of the terrain at coordinate coord.
    /// </summary>
    /// <param name="coord">The coordinate whose terrain data to return.</param>
    /// <returns>The TerrainData of the tile at coord.</returns>
    public TerrainData TerrainAt(Vector2Int coord)
    {
        return TerrainAt(coord.x, coord.y);
    }

    /// <summary>
    ///     Returns the TerrainData of the terrain at coordinate (x, y).
    /// </summary>
    /// <param name="x">The x coordinate of the tile to look for.</param>
    /// <param name="y">The y coordinate of the tile to look for.</param>
    /// <returns>The TerrainData at (x, y).</returns>
    public TerrainData TerrainAt(int x, int y)
    {
        return _grid[x, y];
    }

    /// <summary>
    ///     Returns the (x, y) coordinates of the grid cell that the Vector3 position falls in.
    /// </summary>
    /// <param name="position">yeah</param>
    /// <returns>read the summary</returns>
    public Vector2Int PositionToCoord(Vector3 position)
    {
        var vec3 = _tilemap.WorldToCell(position);
        return new Vector2Int(vec3.x, vec3.y);
    }

    /// <summary>
    ///     Sets the color of a tile to col. Set to Color.white to return to original color.
    /// </summary>
    /// <param name="coord">The location of the tile whose color we are setting.</param>
    /// <param name="col">The color to set the tile to.</param>
    public void SetTileColor(Vector2Int coord, Color col)
    {
        _tilemap.SetColor(coord.ToVector3Int(), col);
    }
}