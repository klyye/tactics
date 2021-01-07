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
    private readonly ISet<Selectable> _units;

    /// <summary>
    ///     Does this player still have units in the game?
    /// </summary>
    public bool alive => _units.Count > 0;

    /// <summary>
    ///     The name of the player.
    /// </summary>
    public readonly string name;

    public Player(string n)
    {
        name = n;
        _units = new HashSet<Selectable>();
    }

    public bool Controls(Selectable unit)
    {
        return _units.Contains(unit);
    }

    public void AddUnit(Selectable unit)
    {
        var def = unit.GetComponent<Defender>();
        if (def)
        {
            def.OnDeath += () => _units.Remove(unit);
        }
        _units.Add(unit);
    }
}
