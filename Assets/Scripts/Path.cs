using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Path
{
    public readonly IEnumerable<Vector2Int> coords;
    public readonly int cost;
    public readonly Vector2Int start, end;

    public Path(Vector2Int initial, Vector2Int dest, IEnumerable<Vector2Int> path = null,
        int pathCost = -1)
    {
        coords = path;
        cost = pathCost;
        start = initial;
        end = dest;
    }

    // two paths are the same if they go from the same start to the same end
    public override bool Equals(object obj)
    {
        if (!(obj is Path item)) return false;
        return item.start == start && item.end == end;
    }

    public override int GetHashCode()
    {
        return start.GetHashCode() + end.GetHashCode() * 487;
    }
}