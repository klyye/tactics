using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gm = GameManager;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Movable))]
public class MoveIssuer : MonoBehaviour
{
    private Movable _movable;
    
    // Start is called before the first frame update
    void Start()
    {
        _movable = GetComponent<Movable>();
    }

    private void OnMouseUpAsButton()
    {
        gm.inputMan.SelectMovable(_movable);
        Debug.Log($"Selected {gameObject.name}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
