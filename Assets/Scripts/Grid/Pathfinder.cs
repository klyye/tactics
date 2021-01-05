using System.Collections.Generic;
using UnityEngine;
using gm = GameManager;

/// <summary>
///     Finds the shortest path between two cells on a given LevelGrid.
/// </summary>
public static class Pathfinder
{
    /// <summary>
    ///     Maps each node on the graph (i.e. a square on the grid) to the total cost of moving
    ///     there from the given starting point. Note: uses double instead of int because we need
    ///     to take advantage of floating point infinity values.
    /// </summary>
    private static IDictionary<Vector2Int, double> _distTo;

    /// <summary>
    ///     Maps each node N on the graph (i.e. a square on the grid) to the node that directly
    ///     precedes N when traveling on the shortest path to N from the given starting point.
    /// </summary>
    private static IDictionary<Vector2Int, Vector2Int> _edgeTo;

    /// <summary>
    ///     The amount of movement points we have left to spend on walking a path from the start.
    /// </summary>
    private static int _pathBudget;

    /// <summary>
    ///     lol don't worry about this its used for the algorithm
    /// </summary>
    private static MinPriorityQueue<Vector2Int> _pq;

    /// <summary>
    ///     The starting point of the shortest paths tree. We are trying to find the shortest path
    ///     starting from this coordinate.
    /// </summary>
    private static Vector2Int _start;

    /// <summary>
    ///     The last LevelGrid that the solving algorithm was run on.
    /// </summary>
    private static LevelGrid _world;

    // returns null on failure
    public static Path ShortestPath(Vector2Int start, Vector2Int end, int pathBudget)
    {
        Solve(start, pathBudget);
        if (!_distTo.ContainsKey(end)) return null;
        var currPath = new List<Vector2Int>();
        var curr = end;
        var pathCost = 0;
        currPath.Add(end);
        while (curr != start)
        {
            pathCost += gm.grid.TerrainAt(curr).moveCost;
            curr = _edgeTo[curr];
            currPath.Add(curr);
        }

        currPath.Reverse();
        return new Path(start, end, currPath, pathCost);
    }

    /// <summary>
    ///     Returns a collection of all reachable coordinates given a starting point and a maximum
    ///     amount of movement points to spend.
    /// </summary>
    /// <param name="start">The point from which you start.</param>
    /// <param name="pathBudget">The maximum amount of movement points to spend.</param>
    /// <returns>see summary</returns>
    public static IEnumerable<Vector2Int> ReachablePoints(Vector2Int start, int pathBudget)
    {
        Solve(start, pathBudget);
        return _distTo.Keys;
    }

    /// <summary>
    ///     Fills the _distTo and _edgeTo Dictionaries with the shortest paths tree from the given
    ///     starting point.
    /// </summary>
    /// <param name="start">The starting point of the shortest paths tree.</param>
    /// <param name="pathBudget">
    ///     The maximum amount of movement points that can be spent in on path.
    /// </param>
    private static void Solve(Vector2Int start, int pathBudget)
    {
        var outdated = _distTo == null || _edgeTo == null || start != _start ||
                       pathBudget != _pathBudget || _world != gm.grid;
        if (!outdated) return;
        _pq = new MinPriorityQueue<Vector2Int>();
        _distTo = new Dictionary<Vector2Int, double>();
        _edgeTo = new Dictionary<Vector2Int, Vector2Int>();
        _start = start;
        _world = gm.grid;
        _pathBudget = pathBudget;

        _pq.Insert(start, 0);
        _distTo[start] = 0;
        _edgeTo[start] = start;

        while (_pq.Size() > 0)
        {
            var p = _pq.RemoveMin();
            foreach (var adj in p.Adjacent())
                if (gm.grid.WithinBounds(adj) && gm.grid.TerrainAt(adj).walkable &&
                    !gm.grid.IsOccupied(adj))
                    Relax(p, adj, pathBudget);
        }
    }

    /// <summary>
    ///     Ahhhhh, relaxation.
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="pathBudget"></param>
    private static void Relax(Vector2Int from, Vector2Int to, int pathBudget)
    {
        var weight = gm.grid.TerrainAt(to).moveCost;
        var distance = DistTo(from) + weight;
        if (distance <= pathBudget && distance < DistTo(to))
        {
            _edgeTo[to] = from;
            _distTo[to] = distance;
            var pri = DistTo(to);
            _pq.Insert(to, pri);
        }
    }

    private static double DistTo(Vector2Int c)
    {
        return _distTo.ContainsKey(c) ? _distTo[c] : double.PositiveInfinity;
    }
}