using UnityEngine;
using gm = GameManager;
using im = InputManager;

/// <summary>
///     Tells the LevelGrid which tiles to highlight, and when.
///     I feel like this class should be static.
/// </summary>
public class GridHighlighter : MonoBehaviour
{
    /// <summary>
    ///     What color to highlight tiles with.
    /// </summary>
    public Color highlight;

    private static Selectable selected => im.selected;

    private void Start()
    {
        im.OnEnterAttackState += () =>
        {
            ClearHighlights();
            HighlightAttackRange();
        };
        im.OnEnterMoveState += () =>
        {
            ClearHighlights();
            HighlightMovementRange();
        };
        im.OnEnterNoneState += ClearHighlights;
    }

    /// <summary>
    ///     Clear all highlights.
    /// </summary>
    private void ClearHighlights()
    {
        for (var x = 0; x < gm.grid.width; x++)
        for (var y = 0; y < gm.grid.height; y++)
            gm.grid.SetTileColor(new Vector2Int(x, y), Color.white);
    }

    /// <summary>
    ///     Highlight the attack range of the currently selected attacker.
    /// </summary>
    private void HighlightAttackRange()
    {
        var atker = selected.GetComponent<Attacker>();
        if (!atker) return;
        var center = gm.grid.PositionToCoord(selected.transform.position);
        var radius = atker.atkRange;
        for (var x = center.x - radius; x < center.x + radius; x++)
        for (var y = center.y - radius; y < center.y + radius; y++)
        {
            var point = new Vector2Int(x, y);
            if (gm.grid.WithinBounds(point))
            {
                var col = center.ManhattanDist(point) < radius ? highlight : Color.white;
                gm.grid.SetTileColor(point, col);
            }
        }
    }

    /// <summary>
    ///     Highlight the movement range of the currently selected mover.
    /// </summary>
    private void HighlightMovementRange()
    {
        var mover = selected.GetComponent<Mover>();
        if (!mover) return;
        var start = gm.grid.PositionToCoord(selected.transform.position);
        var actor = selected.GetComponent<Actor>();
        foreach (var coord in Pathfinder.ReachablePoints(start, actor.actionPoints))
            gm.grid.SetTileColor(coord, highlight);
    }
}