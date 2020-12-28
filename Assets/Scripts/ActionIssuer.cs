using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using gm = GameManager;

/// <summary>
///     Given a Selectable who does the action and either a target square or a target selectable,
///     issue the appropriate action to the action doer (move, attack, etc).
/// </summary>
public static class ActionIssuer 
{
    /// <summary>
    ///     Moves the doer to the target location.
    /// </summary>
    /// <param name="doer">the thing doing the moving</param>
    /// <param name="target">the place to move to</param>
    public static void IssueAction(Selectable doer, Vector2Int target)
    {
        var mover = doer.GetComponent<Mover>();
        if (!mover || mover.locked) return;
        var start = gm.grid.PositionToCoord(mover.transform.position);
        var path = gm.grid.ShortestPath(start, target, doer.actor.actionPoints);
        if (path != null)
            mover.MoveAlong(path);
    }

    /// <summary>
    ///     The doer attacks the target.
    /// </summary>
    /// <param name="doer">The attacker.</param>
    /// <param name="target">The attackee.</param>
    public static void IssueAction(Selectable doer, Selectable target)
    {
        // placeholder for attacks
    }
}
