using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gm = GameManager;
using tm = TurnManager;

/// <summary>
///     An Actor spends action points to do actions.
/// </summary>
public class Actor : MonoBehaviour
{
    /// <summary>
    ///     The points that this actor has left to spend on actions this turn.
    /// </summary>
    [HideInInspector] public int actionPoints;
    
    /// <summary>
    ///     The total number of action points that this actor gets access to on each turn.
    /// </summary>
    [SerializeField] private int _maxPoints;

    private void Start()
    {
        actionPoints = _maxPoints;
        tm.OnNextTurn += () => actionPoints = _maxPoints;
    }
}
