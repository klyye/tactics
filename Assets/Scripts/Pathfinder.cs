﻿using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Finds the shortest path between two cells on a given LevelGrid.
/// </summary>
public class Pathfinder
{
    private readonly Func<Vector2Int, Vector2Int, float> _heuristic;

    // maps a pair <start, end> to the list of coordinates needed to go from start to end
    private readonly IDictionary<Tuple<Vector2Int, Vector2Int>, IList<Vector2Int>> _solution;
    private readonly LevelGrid _world;
    private IDictionary<Vector2Int, float> _distTo;
    private IDictionary<Vector2Int, Vector2Int> _edgeTo;
    private Vector2Int _end;

    private MinPriorityQueue<Vector2Int> _pq;

    public Pathfinder(LevelGrid world)
    {
        _world = world;
        _heuristic = Vector2Int.Distance;
        _solution = new Dictionary<Tuple<Vector2Int, Vector2Int>, IList<Vector2Int>>();
    }

    // returns null on failure
    public IEnumerable<Vector2Int> ShortestPath(Vector2Int start, Vector2Int end)
    {
        var pair = new Tuple<Vector2Int, Vector2Int>(start, end);
        if (_solution.ContainsKey(pair)) return _solution[pair];

        return Solve(start, end) ? _solution[pair] : null;
    }

    private bool Solve(Vector2Int start, Vector2Int end)
    {
        _end = end;
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
                if (_world.WithinBounds(adj) && _world.LandAt(adj).Walkable())
                    Relax(p, adj);
        }

        if (_pq.Size() == 0) return false;

        var curr = end;
        currPath.Add(end);
        while (!curr.Equals(start))
        {
            curr = _edgeTo[curr];
            currPath.Add(curr);
        }

        currPath.Reverse();
        _solution[new Tuple<Vector2Int, Vector2Int>(start, end)] = currPath;
        return true;
    }

    private void Relax(Vector2Int from, Vector2Int to)
    {
        var weight = 1 + (int) _world.LandAt(to);
        if (!(DistTo(from) + weight < DistTo(to))) return;
        _edgeTo[to] = from;
        _distTo[to] = DistTo(from) + weight;
        var pri = DistTo(to) + _heuristic(to, _end);
        _pq.Insert(to, pri);
    }

    private float DistTo(Vector2Int c)
    {
        return _distTo.ContainsKey(c) ? _distTo[c] : float.MaxValue;
    }
}