using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///     Represents whether we are starting the game (i.e. placing units), playing the actual game,
///     or finished with the game (i.e. somebody won)
/// </summary>
public enum GameState 
{
    PRE, PLAYING, POST
}
