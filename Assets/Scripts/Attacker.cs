using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Attackers can deal damage to defenders with their attacks at the cost of action points each
///     turn.
/// </summary>
[RequireComponent(typeof(Actor))]
public abstract class Attacker : MonoBehaviour
{
    /// <summary>
    ///     The actor component of this attacker, stores action points.
    /// </summary>
    private Actor _actor;
    
    private void Start()
    {
        _actor = GetComponent<Actor>();
    }
    
    /// <summary>
    ///     Attacks the target.
    /// </summary>
    /// <param name="target">The target to attack.</param>
    public virtual void Attack(Defender target)
    {
        if (_actor.actionPoints < atkCost) return;
        _actor.actionPoints -= atkCost;
        Debug.Log($"{name} has {_actor.actionPoints} action points left.");
    }
    
    [SerializeField] protected int atkCost;
}
