using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
///     Something (either a human or an AI) that can issue actions to the game.
/// </summary>
public class Player
{
    /// <summary>
    ///     A set of all the Selectables that this Player controls.
    /// </summary>
    private readonly ISet<Actor> _units;

    /// <summary>
    ///     The name of the player.
    /// </summary>
    public readonly string name;

    /// <summary>
    ///     Is this player a computer-controlled AI?
    /// </summary>
    public readonly bool isAI;

    public Player(string n, bool isComputer)
    {
        isAI = isComputer;
        name = n;
        _units = new HashSet<Actor>();
    }

    /// <summary>
    ///     Does this player still have units in the game?
    /// </summary>
    public bool alive => _units.Count > 0;

    public bool Controls(Actor unit)
    {
        return _units.Contains(unit);
    }

    public void AddUnit(Selectable unit)
    {
        var def = unit.GetComponent<Defender>();
        var actor = unit.actor;
        if (def) def.OnDeath += () => _units.Remove(actor);
        _units.Add(actor);
    }

    public IEnumerable<Actor> units => _units;
}