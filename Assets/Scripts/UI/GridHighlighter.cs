using UnityEngine;
using gm = GameManager;

/// <summary>
///     Tells the LevelGrid which tiles to highlight, and when.
///
///     I feel like this class should be static.
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

    private Selectable _selected => gm.inputMan.selected;

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
        var atker = _selected.GetComponent<Attacker>();
        if (!atker) return;
        var center = gm.grid.PositionToCoord(_selected.transform.position);
        var radius = atker.atkRange;
        for (var x = center.x - radius; x < center.x + radius; x++)
        for (var y = center.y - radius; y < center.y + radius; y++)
        {
            var point = new Vector2Int(x, y);
            if (gm.grid.WithinBounds(point))
                _highlighted[x, y] = Vector2Int.Distance(center, point) < radius;
        }

        HighlightGrid();
    }

    /// <summary>
    ///     Highlight the movement range of the currently selected mover.
    /// </summary>
    private void HighlightMovementRange()
    {
        var mover = _selected.GetComponent<Mover>();
        if (!mover) return;
        var start = gm.grid.PositionToCoord(_selected.transform.position);
        var actor = _selected.GetComponent<Actor>();
        Floodfill(start, actor.actionPoints);
        HighlightGrid();
    }

    private void Floodfill(Vector2Int currPos, int pointsLeft)
    {
        var terrain = gm.grid.TerrainAt(currPos);
        var cost = terrain.moveCost;
        if (pointsLeft < 0 || !terrain.walkable) return;
        _highlighted[currPos.x, currPos.y] = true;
        foreach (var adj in currPos.Adjacent())
            if (gm.grid.WithinBounds(adj) && !_highlighted[adj.x, adj.y])
                Floodfill(adj, pointsLeft - cost);
    }
}