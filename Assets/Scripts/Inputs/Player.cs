using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Something (either a human or an AI) that can issue actions to the game.
/// </summary>
public class Player
{
    /// <summary>
    ///     Is this player a computer-controlled AI?
    /// </summary>
    private bool isAI;

    /// <summary>
    ///     A set of all the Selectables that this Player controls.
    /// </summary>
    public readonly ISet<Selectable> units;

    /// <summary>
    ///     The name of the player.
    /// </summary>
    public readonly string name;

    public Player(string n)
    {
        name = n;
        units = new HashSet<Selectable>();
    }
}
