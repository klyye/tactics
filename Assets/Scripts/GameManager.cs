using UnityEngine;
using tm = TurnManager;

/// <summary>
///     ??? does anything that i want it to.
/// </summary>
public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        PRE,
        PLAYING,
        POST
    }

    /// <summary>
    ///     The main camera in this scene.
    /// </summary>
    public static Camera cam;

    /// <summary>
    ///     The one level grid that all the action is taking place on in this scene!
    /// </summary>
    public static LevelGrid grid;

    public static GameState state { get; private set; }

    // Start is called before the first frame update
    private void Awake()
    {
        cam = Camera.main;
        grid = FindObjectOfType<LevelGrid>();
        state = GameState.PRE;
    }
}