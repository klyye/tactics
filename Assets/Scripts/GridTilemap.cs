using System;
using UnityEngine;
using UnityEngine.Tilemaps;

/// <summary>
///     Brings LevelGrids to life using Unity tilemaps.
/// </summary>
[RequireComponent(typeof(Tilemap))]
public class GridTilemap : MonoBehaviour
{
    // TODO this is bad coding. think about a better way
    [SerializeField] private Tile water;
    [SerializeField] private Tile forest;
    [SerializeField] private Tile dirt;
    private Tilemap _tilemap;

    // Start is called before the first frame update
    private void Awake()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    public void UpdateGrid(LevelGrid grid)
    {
        for (var r = 0; r < grid.height; r++)
        for (var c = 0; c < grid.width; c++)
        {
            var land = grid.GetLand(new Vector2Int(c, r));
            switch (land)
            {
                case Land.DIRT:
                    _tilemap.SetTile(new Vector3Int(c, r, 0), dirt);
                    break;
                case Land.WATER:
                    _tilemap.SetTile(new Vector3Int(c, r, 0), water);
                    break;
                case Land.FOREST:
                    _tilemap.SetTile(new Vector3Int(c, r, 0), forest);
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
}