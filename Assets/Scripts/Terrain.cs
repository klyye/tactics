using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Terrain 
{
    // integral value of a terrain is the cost of moving there
    MOUNTAIN = Int32.MaxValue, WATER = 5, GROUND = 1
}
