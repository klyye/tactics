using System;
using System.Collections.Generic;

public class AStarSolver
{
    private IDictionary<Coord, double> _distTo;
    private IDictionary<Coord, Coord> _edgeTo;
    private Coord _end;
    private readonly Func<Coord, Coord, double> _heuristic;

    private MinPriorityQueue<Coord> _pq;

    // maps a pair <start, end> to the list of coordinates needed to go from start to end
    private IDictionary<Tuple<Coord, Coord>, IList<Coord>> _solution;
    private readonly LevelGrid _world;

    public AStarSolver(LevelGrid world, Func<Coord, Coord, double> heuristic)
    {
        _world = world;
        _heuristic = heuristic;
    }

    // returns null on failure
    public IList<Coord> ShortestPath(Coord start, Coord end)
    {
        var pair = new Tuple<Coord, Coord>(start, end);
        if (_solution.ContainsKey(pair)) return _solution[pair];

        return Solve(start, end) ? _solution[pair] : null;
    }

    private bool Solve(Coord start, Coord end)
    {
        _end = end;
        var currPath = new List<Coord>();
        _pq = new MinPriorityQueue<Coord>();
        _distTo = new Dictionary<Coord, double>();
        _edgeTo = new Dictionary<Coord, Coord>();

        _pq.Insert(start, _heuristic.Invoke(start, end));
        _distTo[start] = 0;
        _edgeTo[start] = start;

        while (_pq.Size() > 0 && !_pq.Peek().Equals(end))
        {
            var p = _pq.RemoveMin();
            for (var dx = -1; dx <= 1; dx++)
            for (var dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;
                if (p.x + dx <= _world.width && p.y + dy <= _world.height)
                    Relax(p, new Coord(p.x + dx, p.y + dy));
            }
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
        _solution[new Tuple<Coord, Coord>(start, end)] = currPath;
        return true;
    }

    private void Relax(Coord from, Coord to)
    {
        if (!(DistTo(from) + 1 < DistTo(to))) return;
        _edgeTo[to] = from;
        _distTo[to] = DistTo(from) + 1;
        var pri = DistTo(to) + _heuristic(to, _end);
        _pq.Insert(to, pri);
    }

    private double DistTo(Coord c)
    {
        return _distTo.ContainsKey(c) ? _distTo[c] : double.MaxValue;
    }
}