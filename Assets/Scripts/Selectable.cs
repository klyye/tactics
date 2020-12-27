using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gm = GameManager;

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
        gm.inputMan.Select(this);
        Debug.Log($"Selected {gameObject.name}");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
