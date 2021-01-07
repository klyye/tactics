using UnityEngine;
using gm = GameManager;
using tm = TurnManager;

/// <summary>
///     Given a Selectable who does the action and either a target square or a target selectable,
///     issue the appropriate action to the action doer (move, attack, etc) if it is valid.
/// </summary>
public static class ActionIssuer
{
    /// <summary>
    ///     Moves the doer to the target location.
    /// </summary>
    /// <param name="doer">the thing doing the moving</param>
    /// <param name="target">the place to move to</param>
    public static void IssueMove(Selectable doer, Vector2Int target)
    {
        var mover = doer.GetComponent<Mover>();
        var valid = mover && !mover.locked && tm.currentPlayer.Controls(doer);
        if (!valid) return;
        var start = gm.grid.PositionToCoord(mover.transform.position);
        var path = Pathfinder.ShortestPath(start, target, doer.actor.actionPoints);
        if (path != null)
            mover.MoveAlong(path);
    }

    /// <summary>
    ///     The doer attacks the target.
    /// </summary>
    /// <param name="doer">The attacker.</param>
    /// <param name="target">The attackee.</param>
    public static void IssueAttack(Selectable doer, Selectable target)
    {
        var doercrds = gm.grid.PositionToCoord(doer.transform.position);
        var targetcrds = gm.grid.PositionToCoord(target.transform.position);
        var atker = doer.GetComponent<Attacker>();
        var defnder = target.GetComponent<Defender>();
        // am i supposed to check range here, or in the attack function? Hmm....
        var valid = tm.currentPlayer.Controls(doer) && atker && defnder &&
                    doercrds.ManhattanDist(targetcrds) < atker.atkRange;
        if (!valid) return;
        atker.Attack(defnder);
    }
}