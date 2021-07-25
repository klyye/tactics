using System;
using UnityEngine;

/// <summary>
///     Data about a board that can be saved and loaded via serialization.
/// </summary>
[Serializable]
public class BoardState
{
    [SerializeField] private int width = 9;
    [SerializeField] private int height = 11;
    [SerializeField] private TerrainData[] terrain;
    [SerializeField] public UnitData[] test;
    [SerializeField] private bool isPlayerTurn;

    public BoardState(Defender obj)
    {
        var test1 = new UnitData(obj);
        test = new[] {test1};
        terrain = new TerrainData[width * height];
    }
}

[Serializable]
public struct UnitData
{
    [SerializeField] public GameObject defender;
    [SerializeField] public int health;

    public UnitData(Defender def)
    {
        defender = def.gameObject;
        health = def.hp;
    }
}