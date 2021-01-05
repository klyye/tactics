using System;
using System.Collections.Generic;
using UnityEngine;
using gm = GameManager;

/// <summary>
///     Handles the passage of turns and whose turn it is.
/// </summary>
public sealed class TurnManager : MonoBehaviour
{
    private static IList<Player> _players;
    private static int _turn;

    private void Awake()
    {
        _turn = 0;
        _players = new List<Player>();
    }

    /// <summary>
    ///     The player that is currently playing out their turn.
    /// </summary>
    public static Player currentPlayer => _players[_turn % _players.Count];

    public static event Action OnNextTurn;

    /// <summary>
    ///     Adds a player to the list of players taking turns.
    /// </summary>
    /// <param name="player">The player to add.</param>
    /// <param name="index">Where that player falls in the turn order.</param>
    public static void AddPlayer(Player player, int index)
    {
        _players.Insert(index, player);
    }

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