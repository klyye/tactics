using UnityEngine;
using System;

/// <summary>
///     Defenders have health and can take damage.
/// </summary>
public class Defender : MonoBehaviour
{
    /// <summary>
    ///     The total amount of hp this defender can have.
    /// </summary>
    [SerializeField] private int _maxHp;

    /// <summary>
    ///     Hit points. Classic video game mechanic.
    /// </summary>
    private int _hp;

    /// <summary>
    ///     Event that fires once this defender dies.
    /// </summary>
    public event Action OnDeath;

    public int hp => _hp;
    public int maxHp => _maxHp;

    // Start is called before the first frame update
    private void Start()
    {
        _hp = _maxHp;
        HealthBars.inst.AddDefender(this);
    }

    /// <summary>
    ///     Hit this defender for dmg damage, taking away its health.
    ///     Will trigger any on-hit effects.
    /// </summary>
    /// <param name="dmg">The amount of damage to deal.</param>
    public void TakeDamage(int dmg)
    {
        _hp = Math.Min(_hp - dmg, maxHp);
        if (_hp <= 0) Die();
    }

    public void SetHealth(int health)
    {
        _hp = Math.Min(health, maxHp);
    }

    /// <summary>
    ///     Die.
    /// </summary>
    public void Die()
    {
        Destroy(gameObject);
        OnDeath?.Invoke();
    }
}