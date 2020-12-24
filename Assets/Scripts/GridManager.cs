using UnityEngine;

public class GridManager : MonoBehaviour
{
    private static LevelGrid _gameGrid;

    public static GridTilemap tilemap;
    public static AStarSolver pathfinder;
    [SerializeField] private int width;
    [SerializeField] private int height;

    // Start is called before the first frame update
    private void Start()
    {
        tilemap = FindObjectOfType<GridTilemap>();
        _gameGrid = new LevelGrid(width, height);
        _gameGrid.SetTerrain(new Vector2Int(1, 1), Land.WATER);
        _gameGrid.SetTerrain(new Vector2Int(1, 2), Land.WATER);
        _gameGrid.SetTerrain(new Vector2Int(1, 3), Land.WATER);
        _gameGrid.SetTerrain(new Vector2Int(1, 4), Land.WATER);

        _gameGrid.SetTerrain(new Vector2Int(3, 0), Land.WATER);
        _gameGrid.SetTerrain(new Vector2Int(3, 1), Land.WATER);
        _gameGrid.SetTerrain(new Vector2Int(3, 2), Land.WATER);
        _gameGrid.SetTerrain(new Vector2Int(3, 3), Land.WATER);
        for (var x = 0; x < width; x++)
        {
            _gameGrid.SetTerrain(new Vector2Int(x, 5), Land.WATER);
        }

        for (var y = 0; y < height; y++)
        {
            _gameGrid.SetTerrain(new Vector2Int(5, y), Land.WATER);
        }

        pathfinder = new AStarSolver(_gameGrid);
    }

    // Update is called once per frame
    private void Update()
    {
        tilemap.UpdateGrid(_gameGrid);
    }
}