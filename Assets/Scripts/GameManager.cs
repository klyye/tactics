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
        var save = new Save(SceneManager.GetActiveScene().buildIndex);
        var json = JsonUtility.ToJson(save, true);
        Debug.Log(Application.persistentDataPath + SAVE_PATH);
        File.WriteAllText(Application.persistentDataPath + SAVE_PATH, json);
    }

    public void LoadState()
    {
        var json = File.ReadAllText(Application.persistentDataPath + SAVE_PATH);
        var save = (Save)JsonUtility.FromJson(json, typeof(Save));
        SceneManager.LoadScene(save.currLevel);
    }

    public void Quit()
    {
        Application.Quit();
    }
}