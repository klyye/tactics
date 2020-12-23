using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2IntExtension
{
    public static IEnumerable<Vector2Int> Adjacent(this Vector2Int vec)
    {
        yield return vec + Vector2Int.down;
        yield return vec + Vector2Int.left;
        yield return vec + Vector2Int.right;
        yield return vec + Vector2Int.up;
    }
}
