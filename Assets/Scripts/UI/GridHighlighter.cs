using System;
using UnityEngine;
using gm = GameManager;

/// <summary>
///     Tells the LevelGrid which tiles to highlight, and when.
/// </summary>
public class GridHighlighter
{
    /// <summary>
    ///     Is true at (x, y) if that tile should be highlighted on the next call to HighlightGrid.
    /// </summary>
    private readonly bool[,] _highlighted;

    public GridHighlighter()
    {
        gm.inputMan.OnEnterAttackState += HighlightAttackRange;
        gm.inputMan.OnEnterMoveState += HighlightMovementRange;
        gm.inputMan.OnEnterNoneState += ClearHighlights;
        _highlighted = new bool[gm.grid.width, gm.grid.height];
        ClearHighlights();
    }

    /// <summary>
    ///     Highlight tiles on the game grid based on the data in _highlighted.
    /// </summary>
    private void HighlightGrid()
    {
        for (var x = 0; x < gm.grid.width; x++)
        for (var y = 0; y < gm.grid.height; y++)
        {
            var pos = new Vector2Int(x, y);
            if (_highlighted[x, y])
                gm.grid.HighlightTile(pos);
            else
                gm.grid.UnhighlightTile(pos);
        }
    }

    /// <summary>
    ///     Clear all currently planned highlights.
    /// </summary>
    private void ClearHighlights()
    {
        for (var x = 0; x < gm.grid.width; x++)
        for (var y = 0; y < gm.grid.height; y++)
            _highlighted[x, y] = false;
        HighlightGrid();
    }

    /// <summary>
    ///     Highlight the attack range of the currently selected attacker.
    /// </summary>
    private void HighlightAttackRange()
    {
        var atker = gm.inputMan.selected.GetComponent<Attacker>();
        if (!atker) return;
        var center = gm.grid.PositionToCoord(gm.inputMan.selected.transform.position);
        var radius = atker.atkRange;
        for (var x = Math.Max(center.x - radius, 0);
            x < Math.Min(gm.grid.width, center.x + radius);
            x++)
        for (var y = Math.Max(center.y - radius, 0);
            y < Math.Min(gm.grid.height, center.y + radius);
            y++)
        {
            var point = new Vector2Int(x, y);
            _highlighted[x, y] = Vector2Int.Distance(center, point) < radius;
        }
        HighlightGrid();
    }

    /// <summary>
    ///     Highlight the movement range of the currently selected mover.
    /// </summary>
    private void HighlightMovementRange()
    {
    }
}