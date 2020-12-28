using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gm = GameManager;

/// <summary>
///     Handles the passage of turns and whose turn it is.
/// </summary>
public sealed class TurnManager 
{
    private int _turn;
    public event Action OnNextTurn;

    /// <summary>
    ///     The singleton instance of this class.
    /// </summary>
    public static TurnManager inst { get; } = new TurnManager();

    private TurnManager()
    {
        _turn = 0;
    }

    /// <summary>
    ///     Go to the next turn.
    /// </summary>
    public void AdvanceTurn()
    {
        _turn++;
        OnNextTurn?.Invoke();
        
    }

}