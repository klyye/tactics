using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gm = GameManager;

public sealed class TurnManager 
{
    private Queue<Action> _actionQueue;
    private int _turn;
    public static TurnManager inst { get; } = new TurnManager();

    private TurnManager()
    {
        _turn = 0;
        _actionQueue = new Queue<Action>();
    }

    public void IssueCommand(Action act)
    {
        _actionQueue.Enqueue(act);
    }

    public void AdvanceTurn()
    {
        _turn++;
        while (_actionQueue.Count > 0)
        {
            var act = _actionQueue.Dequeue();
            act.Invoke();
        }
        gm.grid.UpdateTilemap();
    }
}