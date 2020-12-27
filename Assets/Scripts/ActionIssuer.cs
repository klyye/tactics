using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using gm = GameManager;

public static class ActionIssuer 
{
    public static void IssueAction(Selectable doer, Vector2Int target)
    {
        var mover = doer.GetComponent<Mover>();
        if (!mover || mover.locked) return;
        var start = gm.grid.PositionToCoord(mover.transform.position);
        var path = gm.grid.ShortestPath(start, target, doer.actor.actionPoints);
        if (path != null)
            mover.MoveAlong(path);
    }

    public static void IssueAction(Selectable doer, Selectable target)
    {
        // placeholder for attacks
    }
}
