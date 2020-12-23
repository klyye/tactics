using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap))]
public class GridTilemap : MonoBehaviour
{
    private Tilemap _tilemap;
    
    // TODO this is bad coding. think about a better way
    [SerializeField] private Tile water;
    [SerializeField] private Tile forest;
    [SerializeField] private Tile dirt;
    
    public void UpdateGrid(LevelGrid grid)
    {
        for (var r = 0; r < grid.height; r++)
        {
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
    }
    
    // Start is called before the first frame update
    void Start()
    {
        _tilemap = GetComponent<Tilemap>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
