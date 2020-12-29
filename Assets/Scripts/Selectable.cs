using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gm = GameManager;
using tm = TurnManager;

/// <summary>
///     Something you can click on which tells the InputManager that it was selected.
/// </summary>
[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Actor))]
public class Selectable : MonoBehaviour
{
    private Actor _actor;
    public Actor actor => _actor;
    
    // Start is called before the first frame update
    void Start()
    {
        _actor = GetComponent<Actor>();
    }

    private void OnMouseUpAsButton()
    {
        if (tm.inst.currentPlayer.Controls(this)) 
            gm.inputMan.Select(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
