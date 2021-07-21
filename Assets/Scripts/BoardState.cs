using System;
using UnityEngine;

/// <summary>
///     The current state of the board?? (self-explanatory?)
/// </summary>
[Serializable]
public class BoardState
{
    [SerializeField] private int width = 9;
    [SerializeField] private int height = 11;
    [SerializeField] private TerrainData[] terrain = new TerrainData[1];
}