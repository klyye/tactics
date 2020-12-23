using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Location
{
    public Unit unit;
    public Terrain terrain;

    public Location(Unit u, Terrain f)
    {
        unit = u;
        terrain = f;
    }
}
