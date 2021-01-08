using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///   Used to carry the options that the player puts into the main menu into the actual game.
///   Persists across scenes.
/// </summary>
public class MainMenuData : MonoBehaviour
{
   public static List<Player> players { get; private set; }
   private void Awake()
   {
      DontDestroyOnLoad(gameObject);
      players = new List<Player>();
      players.Add(new Player("Human player", false));
      players.Add(new Player("AI", true));
   }
}
