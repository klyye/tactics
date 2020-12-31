using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Some additional functionality for unity's Vector2Int.
/// </summary>
public static class Vector2IntExtension
{
    /// <summary>
    ///     A generator function that yields the Vector2Ints adjacent (up, down, left, right) to
    ///     this one.
    /// </summary>
    /// <param name="vec">The Vector2Int whose adjacent coordinates we are yielding.</param>
    /// <returns>lol</returns>
    public static IEnumerable<Vector2Int> Adjacent(this Vector2Int vec)
    {
        yield return vec + Vector2Int.down;
        yield return vec + Vector2Int.left;
        yield return vec + Vector2Int.right;
        yield return vec + Vector2Int.up;
    }

    /// <summary>
    ///     Converts this Vector2Int into a Vector3 with the z axis at 0.
    /// </summary>
    /// <param name="vec">The Vector2Int to be converted.</param>
    /// <returns>A Vector3 with the same x and y as this Vector2Int and z axis at 0.</returns>
    public static Vector3Int ToVector3Int(this Vector2Int vec)
    {
        return new Vector3Int(vec.x, vec.y, 0);
    }
}
