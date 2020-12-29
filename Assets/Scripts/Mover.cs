using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using gm = GameManager;
using tm = TurnManager;

[RequireComponent(typeof(Actor))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float waitTime;
    [SerializeField] private float speed;
    private Actor _actor;
    private bool _locked;
    public bool locked => _locked;
    public Vector2Int coords => gm.grid.PositionToCoord(transform.position);

    private void Start()
    {
        _locked = false;
        _actor = GetComponent<Actor>();
        tm.inst.OnNextTurn += () => _locked = false;
        gm.grid.Occupy(coords);
    }

    private void OnDestroy()
    {
        if (gm.grid)
            gm.grid.Unoccupy(coords);
    }

    public void MoveAlong(Path path)
    {
        if (_actor.actionPoints < path.cost) return;
        transform.position = gm.grid.CoordToPosition(path.start);
        _actor.actionPoints -= path.cost;
        Debug.Log($"{name} has {_actor.actionPoints} action points left.");
        gm.grid.Unoccupy(path.start);
        gm.grid.Occupy(path.end);
        StartCoroutine(FollowPath(path.coords));
    }

    private IEnumerator FollowPath(IEnumerable<Vector2Int> path)
    {
        _locked = true;
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

        _locked = false;
    }
}