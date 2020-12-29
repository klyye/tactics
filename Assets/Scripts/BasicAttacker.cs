using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     An Attacker that simply does a set amount of damage to the target.
/// </summary>
public class BasicAttacker : Attacker
{
    /// <summary>
    ///     How much damage the basic attack should do.
    /// </summary>
    [SerializeField] private int damage;

    /// <summary>
    ///     Deals a set amount of damage to the target.
    /// </summary>
    /// <param name="target"></param>
    public override void Attack(Defender target)
    {
        base.Attack(target);
        target.TakeDamage(damage);
    }
    
}
