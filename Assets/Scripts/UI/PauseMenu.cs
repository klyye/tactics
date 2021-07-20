using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static PauseMenu inst { get; private set; }
    public static bool paused { get; private set; }

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
            paused = false;
        }
    }

    public static void TogglePause()
    {
        inst.gameObject.SetActive(!inst.gameObject.activeInHierarchy);
        paused = !paused;
        Time.timeScale = Time.timeScale > 0 ? 0 : 1;
    }
}