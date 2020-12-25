using System.Collections.Generic;
using System.Linq;
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

    [SerializeField] private TerrainData[] _terrains;
    private TerrainData[,] _grid;
    private Pathfinder _pathfinder;
    private Tilemap _tilemap;

    // Start is called before the first frame update
    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
        _grid = new TerrainData[width, height];
        var walkables = _terrains.Where(l => l.walkable).ToArray();
        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
        {
            var roll = Rand.value;
            if (roll > 0.75)
                _grid[x, y] = walkables[Rand.Range(0, walkables.Length)];
            else
                _grid[x, y] = _terrains[Rand.Range(0, _terrains.Length)];
        }

        _grid[width - 1, height - 1] = walkables[Rand.Range(0, walkables.Length)];
        UpdateTilemap();
        _pathfinder = new Pathfinder(this);
    }

    public IEnumerable<Vector2Int> ShortestPath(Vector2Int start, Vector2Int end)
    {
        return _pathfinder.ShortestPath(start, end);
    }

    public void UpdateTilemap()
    {
        for (var x = 0; x < width; x++)
        for (var y = 0; y < height; y++)
        {
            var land = LandAt(x, y);
            _tilemap.SetTile(new Vector3Int(x, y, 0), land.tile);
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

    public TerrainData LandAt(Vector2Int coord)
    {
        return LandAt(coord.x, coord.y);
    }

    public TerrainData LandAt(int x, int y)
    {
        return _grid[x, y];
    }
}