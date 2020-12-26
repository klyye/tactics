using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gm = GameManager;

[RequireComponent(typeof(Collider2D))]
[RequireComponent(typeof(Mover))]
public class MoveIssuer : MonoBehaviour
{
    private Mover _mover;
    
    // Start is called before the first frame update
    void Start()
    {
        _mover = GetComponent<Mover>();
    }

    private void OnMouseUpAsButton()
    {
        gm.inputMan.SelectMovable(_mover);
        Debug.Log($"Selected {gameObject.name}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
