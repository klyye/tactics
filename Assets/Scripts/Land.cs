using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// credit to https://opengameart.org/content/16x16-tileset-water-dirt-forest
/// <summary>
///     Represents the different types of land that a tile can take on.
/// </summary>
public enum Land 
{
    // integral value of a land is the cost of moving there
    WATER = 9999, FOREST = 5, DIRT = 1
}
