using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public readonly ISet<Unit> ownedUnits;
    public readonly string name;

    public Player(string n)
    {
        ownedUnits = new HashSet<Unit>();
        name = n;
    }
    
}
