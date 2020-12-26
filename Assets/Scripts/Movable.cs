using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gm = GameManager;

public class Movable : MonoBehaviour
{
    [SerializeField] private float waitTime;
    [SerializeField] private float speed;
    
    public void MoveTo(Vector2Int dest)
    {
        Debug.Log($"Moving {name} to ({dest.x}, {dest.y})");
        var start = gm.grid.PositionToCoord(transform.position);
        transform.position = gm.grid.CoordToPosition(start);
        var path = gm.grid.ShortestPath(start, dest);
        if (path != null)
            StartCoroutine(FollowPath(path));
    }

    private IEnumerator FollowPath(IEnumerable<Vector2Int> path)
    {
        foreach (var coord in path)
        {
            var next = gm.grid.CoordToPosition(coord);
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
