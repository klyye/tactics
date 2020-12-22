using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Location
{
    public Unit unit;
    public Feature feature;

    public Location(Unit u, Feature f)
    {
        unit = u;
        feature = f;
    }
}
