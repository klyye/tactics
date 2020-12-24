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
    // even values are walkable. odd values are not. is this janky?
    WATER = 9999, FOREST = 4, DIRT = 0
}

static class LandMethods
{
    public static bool Walkable(this Land l)
    {
        return (int) l % 2 == 0;
    }
}
