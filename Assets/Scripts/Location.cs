using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Location
{
    public Unit unit;
    public Land land;

    public Location(Unit u, Land f)
    {
        unit = u;
        land = f;
    }
}
