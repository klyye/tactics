using System;
using System.Collections.Generic;
using UnityEngine;
using gm = GameManager;
using pm = PlayerManager;

/// <summary>
///     Handles the passage of turns and whose turn it is.
/// </summary>
public sealed class TurnManager : MonoBehaviour
{
    private static int _turn;

    private void Awake()
    {
        _turn = 0;
    }

    /// <summary>
    ///     The player that is currently playing out their turn.
    /// </summary>
    public static Player currentPlayer => pm.players[_turn % pm.players.Count];

    public static event Action OnNextTurn;

    /// <summary>
    ///     Go to the next turn.
    /// </summary>
    public static void AdvanceTurn()
    {
        _turn++;
        Debug.Log($"Turn {_turn}: Player {currentPlayer.name} is in control.");
        OnNextTurn?.Invoke();
    }
}