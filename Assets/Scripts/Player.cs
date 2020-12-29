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
    private ISet<Selectable> _controlled;

    /// <summary>
    ///     The name of the player.
    /// </summary>
    public readonly string name;

    public Player(string n)
    {
        name = n;
        _controlled = new HashSet<Selectable>();
    }

    /// <summary>
    ///     Adds a selectable to this player's set of controlled selectables.
    /// </summary>
    /// <param name="s">The selectable to add under this player's control</param>
    public void Control(Selectable s)
    {
        _controlled.Add(s);
    }

    /// <summary>
    ///     Does this player control the selectable s?
    /// </summary>
    /// <param name="s">The selectable that we are checking</param>
    /// <returns>xd</returns>
    public bool Controls(Selectable s)
    {
        return _controlled.Contains(s);
    }
}
