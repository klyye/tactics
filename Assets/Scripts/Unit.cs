using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Represents a thing that can move around on the field and do combat.
/// </summary>
public class Unit : MonoBehaviour
{
    [SerializeField] private float waitTime;
    [SerializeField] private float speed;
    private LevelGrid _levelGrid;

    private void Start()
    {
        _levelGrid = GameManager.levelGrid;
    }

    public void Move(Vector2Int start, Vector2Int dest)
    {
        transform.position = _levelGrid.CoordToPosition(start);
        var path = _levelGrid.ShortestPath(start, dest);
        if (path != null)
            StartCoroutine(FollowPath(path));
    }

    private IEnumerator FollowPath(IEnumerable<Vector2Int> path)
    {
        foreach (var coord in path)
        {
            var next = _levelGrid.CoordToPosition(coord);
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