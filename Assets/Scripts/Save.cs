using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Save
{
    public int currLevel = 0;

    public Save(int clvl)
    {
        currLevel = clvl;
    }
}
