using System;
using UnityEngine;
using gm = GameManager;
using pm = PlayerManager;

/// <summary>
///     Handles the passage of turns and whose turn it is.
/// </summary>
public sealed class TurnManager : MonoBehaviour
{
    public static int turn { get; private set; }

    /// <summary>
    ///     The player that is currently playing out their turn.
    /// </summary>
    public static Player currentPlayer => pm.players[turn % pm.players.Count];

    private void Awake()
    {
        turn = 0;
    }

    public static event Action OnNextTurn;

    /// <summary>
    ///     Go to the next turn.
    /// </summary>
    public static void AdvanceTurn()
    {
        turn++;
        OnNextTurn?.Invoke();
    }
}