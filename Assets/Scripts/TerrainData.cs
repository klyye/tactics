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

    /// <summary>
    ///     The tile asset that corresponds to this type of terrain.
    /// </summary>
    public Tile tile => _tile;
    /// <summary>
    ///     How many action points does it cost to move here?
    /// </summary>
    public int moveCost => _moveCost;
    /// <summary>
    ///     Can you walk on this type of terrain?
    /// </summary>
    public bool walkable => _walkable;

}