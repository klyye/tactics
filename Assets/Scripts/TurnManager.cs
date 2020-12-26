using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gm = GameManager;

public sealed class TurnManager 
{
    private int _turn;
    public event Action OnNextTurn;

    public static TurnManager inst { get; } = new TurnManager();

    private TurnManager()
    {
        _turn = 0;
    }


    public void AdvanceTurn()
    {
        _turn++;
        OnNextTurn?.Invoke();
        
    }

}