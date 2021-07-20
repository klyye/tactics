using System;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu inst;
    
    public void Awake()
    {
        if (inst)
        {
            Destroy(gameObject);
        }
        else
        {
            inst = this;
            gameObject.SetActive(false);
        }
    }

    public static void TogglePause()
    {
        inst.gameObject.SetActive(!inst.gameObject.activeInHierarchy);
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
    }
}