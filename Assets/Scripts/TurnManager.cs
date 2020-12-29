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
    private IList<Player> _players;

    /// <summary>
    ///     The singleton instance of this class.
    /// </summary>
    public static TurnManager inst { get; } = new TurnManager();

    /// <summary>
    ///     The player that is currently playing out their turn.
    /// </summary>
    public Player currentPlayer => _players[_turn % _players.Count];

    private TurnManager()
    {
        _turn = 0;
        _players = new List<Player>();
    }

    /// <summary>
    ///     Adds a player to the list of players taking turns.
    /// </summary>
    /// <param name="player">The player to add.</param>
    /// <param name="index">Where that player falls in the turn order.</param>
    public void AddPlayer(Player player, int index)
    {
        _players.Insert(index, player);
    }

    /// <summary>
    ///     Go to the next turn.
    /// </summary>
    public void AdvanceTurn()
    {
        _turn++;
        Debug.Log($"Turn {_turn}: Player {currentPlayer.name} is in control.");
        OnNextTurn?.Invoke();
    }

}