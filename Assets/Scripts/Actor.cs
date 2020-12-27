using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gm = GameManager;
using tm = TurnManager;

public class Actor : MonoBehaviour
{
    [HideInInspector] public int actionPoints;
    [SerializeField] private int _maxPoints;

    private void Start()
    {
        actionPoints = _maxPoints;
        tm.inst.OnNextTurn += () => actionPoints = _maxPoints;
    }
}
