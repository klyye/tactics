using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Represents a thing that can move around on the field and do combat.
/// </summary>
public class Unit : MonoBehaviour
{
    public float waitTime;
    public float speed;
    public Vector2Int start, dest;

    private void Start()
    {
        // TODO THIS IS ALL TEMPORARY LMAO
        transform.position = GridManager.tilemap.CoordToPosition(start);
        StartCoroutine(FollowPath(GridManager.pathfinder.ShortestPath(start, dest)));
    }

    private IEnumerator FollowPath(IEnumerable<Vector2Int> path)
    {
        foreach (var coord in path)
        {
            var next = GridManager.tilemap.CoordToPosition(coord);
            while (Vector3.Distance(transform.position, next) > 0.01)
            {
                transform.position = Vector3.MoveTowards(transform.position, next,
                    speed * Time.deltaTime);
                yield return null;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
}