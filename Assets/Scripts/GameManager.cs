using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using tm = TurnManager;
using pm = PlayerManager;

/// <summary>
///     ??? does anything that i want it to.
/// </summary>
public class GameManager : MonoBehaviour
{
    public const string SAVE_PATH = "/save";
        
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
        pm.OnAllUnitsPlaced += () => state = GameState.PLAYING;
        pm.OnOnePlayerRemaining += () => state = GameState.POST;
    }

    public void SaveState()
    {
        var actor = FindObjectOfType<Defender>();
        Debug.Log($"Found actor {actor.name}");
        var state = new BoardState(actor);
        var json = JsonUtility.ToJson(state, true);
        File.WriteAllText(Application.persistentDataPath + SAVE_PATH, json);
    }

    public void LoadState()
    {
        var json = File.ReadAllText(Application.persistentDataPath + SAVE_PATH);
        var state = (BoardState)JsonUtility.FromJson(json, typeof(BoardState));
        var def = state.test[0].defender.GetComponent<Defender>();
        var spawned = Instantiate(def, Vector3.zero, Quaternion.identity);
        spawned.SetHealth(1);
        Debug.Log($"Loaded actor {def.name}");
    }

    public void Quit()
    {
        Application.Quit();
    }
}