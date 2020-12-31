﻿using UnityEngine;
using System.Collections.Generic;
using tm = TurnManager;

/// <summary>
///     ??? does anything that i want it to.
/// </summary>
public class GameManager : MonoBehaviour
{
    /// <summary>
    ///     The main camera in this scene.
    /// </summary>
    public static Camera cam;
    
    /// <summary>
    ///     The one level grid that all the action is taking place on in this scene!
    /// </summary>
    public static LevelGrid grid;
    
    /// <summary>
    ///     The one input manager that handles all the input in this scene!
    /// </summary>
    public static InputManager inputMan;

    /// <summary>
    ///     TODO: TEMPORARY WAY TO GET PLAYERS INTO THE GAME VIA UNITY INSPECTOR
    /// </summary>
    [SerializeField] private string[] playerNames;

    // Start is called before the first frame update
    private void Awake()
    {
        cam = Camera.main;
        inputMan = FindObjectOfType<InputManager>();
        grid = FindObjectOfType<LevelGrid>();
        for (var i = 0; i < playerNames.Length; i++)
        {
            var p = new Player(playerNames[i]);
            // todo: placeholder stuff
            var fireman = GameObject.Find("Flame Elemental").GetComponent<Selectable>();
            var waterman = GameObject.Find("Water Elemental").GetComponent<Selectable>();
            p.units.Add(i % 2 == 0 ? fireman : waterman);
            tm.inst.AddPlayer(p, i);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tm.inst.AdvanceTurn();
        }
    }
}