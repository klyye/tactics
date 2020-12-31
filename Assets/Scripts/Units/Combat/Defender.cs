using UnityEngine;

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

    // Start is called before the first frame update
    private void Start()
    {
        _hp = _maxHp;
    }

    /// <summary>
    ///     Hit this defender for dmg damage, taking away its health.
    /// </summary>
    /// <param name="dmg">The amount of damage to deal.</param>
    public void TakeDamage(int dmg)
    {
        _hp -= dmg;
        Debug.Log($"Ow! {name} took {dmg} damage and has {_hp} hp left!");
        if (_hp <= 0) Die();
    }

    /// <summary>
    ///     Die.
    /// </summary>
    public void Die()
    {
        Destroy(gameObject);
    }
}