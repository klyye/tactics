using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Finds the shortest path between two cells on a given LevelGrid.
/// </summary>
public class Pathfinder
{
    private readonly Func<Vector2Int, Vector2Int, float> _heuristic;

    // https://stackoverflow.com/questions/7760364/how-to-retrieve-actual-item-from-hashsett ????
    private readonly IDictionary<Path, Path> _solution;
    private readonly LevelGrid _world;
    private IDictionary<Vector2Int, float> _distTo;
    private IDictionary<Vector2Int, Vector2Int> _edgeTo;
    private Vector2Int _end;

    private MinPriorityQueue<Vector2Int> _pq;

    public Pathfinder(LevelGrid world)
    {
        _world = world;
        _heuristic = Vector2Int.Distance;
        _solution = new Dictionary<Path, Path>();
    }

    // returns null on failure
    public Path ShortestPath(Vector2Int start, Vector2Int end, int pathBudget)
    {
        var path = new Path(start, end);
        if (_solution.ContainsKey(path)) return _solution[path];
        path = Solve(start, end);
        if (path == null || path.cost > pathBudget) return null;
        return path;
    }

    private Path Solve(Vector2Int start, Vector2Int end)
    {
        _end = end;
        var pathCost = 0;
        var currPath = new List<Vector2Int>();
        _pq = new MinPriorityQueue<Vector2Int>();
        _distTo = new Dictionary<Vector2Int, float>();
        _edgeTo = new Dictionary<Vector2Int, Vector2Int>();

        _pq.Insert(start, _heuristic.Invoke(start, end));
        _distTo[start] = 0;
        _edgeTo[start] = start;

        while (_pq.Size() > 0 && !_pq.Peek().Equals(end))
        {
            var p = _pq.RemoveMin();
            foreach (var adj in p.Adjacent())
                if (_world.WithinBounds(adj) && _world.TerrainAt(adj).walkable &&
                    !_world.IsOccupied(adj))
                {
                    var weight = _world.TerrainAt(p).moveCost;
                    if (DistTo(p) + weight < DistTo(adj))
                    {
                        _edgeTo[adj] = p;
                        _distTo[adj] = DistTo(p) + weight;
                        var pri = DistTo(adj) + _heuristic(adj, _end);
                        _pq.Insert(adj, pri);
                    }
                }
        }

        if (_pq.Size() == 0) return null;

        var curr = end;
        currPath.Add(end);
        while (!curr.Equals(start))
        {
            pathCost += _world.TerrainAt(curr).moveCost;
            curr = _edgeTo[curr];
            currPath.Add(curr);
        }

        currPath.Reverse();
        var output = new Path(start, end, currPath, pathCost);
        _solution[output] = output;
        return output;
    }

    private float DistTo(Vector2Int c)
    {
        return _distTo.ContainsKey(c) ? _distTo[c] : float.MaxValue;
    }
}