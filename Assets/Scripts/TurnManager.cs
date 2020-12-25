using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public void EnqueueAction(Action act)
    {
        _actionQueue.Enqueue(act);
    }

    public void AdvanceTurn()
    {
        _turn++;
        for (var i = 0; i < _actionQueue.Count; i++)
        {
            var act = _actionQueue.Dequeue();
            act.Invoke();
        }
    }
}