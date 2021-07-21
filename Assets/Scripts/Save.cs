using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public int currLevel = 0;
    public Path test;

    public Save(int clvl)
    {
        currLevel = clvl;
        test = new Path(Vector2Int.down, Vector2Int.down);
    }
}
