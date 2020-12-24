using UnityEngine;

/// <summary>
///     ??? does anything that i want it to.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static Camera cam;
    public static LevelGrid levelGrid;

    // Start is called before the first frame update
    private void Awake()
    {
        cam = Camera.main;
        levelGrid = FindObjectOfType<LevelGrid>();
    }

    // Update is called once per frame
    private void Update()
    {
    }
}