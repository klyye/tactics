using System;
using UnityEngine;
using UnityEngine.Tilemaps;

// credit to https://opengameart.org/content/16x16-tileset-water-dirt-forest
/// <summary>
///     Represents the different types of land that a tile can take on.
/// </summary>
[CreateAssetMenu(fileName = "New TerrainData", menuName = "Terrain Data", order = 51)]
public class TerrainData : ScriptableObject
{
    [SerializeField] private int _moveCost;
    [SerializeField] private Tile _tile;
    [SerializeField] private bool _walkable;

    public Tile tile => _tile;
    public int moveCost => _moveCost;
    public bool walkable => _walkable;

}