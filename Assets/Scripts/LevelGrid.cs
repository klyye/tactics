using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Rand = UnityEngine.Random;

/// <summary>
///     Brings LevelGrids to life using Unity tilemaps.
/// </summary>
[RequireComponent(typeof(Tilemap))]
public class LevelGrid : MonoBehaviour
{
    public int width;
    public int height;

    // TODO this is bad coding. think about a better way
    [SerializeField] private Tile water;
    [SerializeField] private Tile forest;
    [SerializeField] private Tile dirt;
    private Tilemap _tilemap;
    private Land[,] _grid;
    public Pathfinder pathfinder;

    // Start is called before the first frame update
    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
        _grid = new Land[width, height];
        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
            _grid[x, y] = Rand.value > 0.75 ? Land.WATER : default;
        UpdateTilemap();
        pathfinder = new Pathfinder(this);
    }

    public void UpdateTilemap()
    {
        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
        {
            var land = _grid[x, y];
            switch (land)
            {
                case Land.DIRT:
                    _tilemap.SetTile(new Vector3Int(x, y, 0), dirt);
                    break;
                case Land.WATER:
                    _tilemap.SetTile(new Vector3Int(x, y, 0), water);
                    break;
                case Land.FOREST:
                    _tilemap.SetTile(new Vector3Int(x, y, 0), forest);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    public Vector3 CoordToPosition(Vector2Int cellPosition)
    {
        return _tilemap.GetCellCenterWorld(cellPosition.ToVector3Int());
    }

    public bool WithinBounds(Vector2Int coord)
    {
        return coord.x >= 0 && coord.y >= 0 && coord.x < width && coord.y < height;
    }

    public Land LandAt(Vector2Int coord)
    {
        return _grid[coord.x, coord.y];
    }
}