using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// credit to https://opengameart.org/content/16x16-tileset-water-dirt-forest
public enum Land 
{
    // integral value of a land is the cost of moving there
    WATER = int.MaxValue, FOREST = 5, DIRT = 1
}
