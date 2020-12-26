using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using gm = GameManager;

public class Mover : MonoBehaviour
{
    [SerializeField] private float waitTime;
    [SerializeField] private float speed;
    [SerializeField] private int _movePoints;
    public int movePoints => _movePoints;
    public Action OnActionEnd;

    public void MoveAlong(IEnumerable<Vector2Int> path)
    {
        var start = gm.grid.PositionToCoord(transform.position);
        transform.position = gm.grid.CoordToPosition(start);
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
        OnActionEnd?.Invoke();
    }
}
