using UnityEngine;
using System.Collections.Generic;
using tm = TurnManager;

/// <summary>
///     ??? does anything that i want it to.
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private int numPlayers;
    public static Camera cam;
    public static LevelGrid grid;
    public static InputManager inputMan;
    public static IList<Player> players;

    // Start is called before the first frame update
    private void Awake()
    {
        cam = Camera.main;
        inputMan = FindObjectOfType<InputManager>();
        grid = FindObjectOfType<LevelGrid>();
        players = new List<Player>();
        for (var i = 0; i < numPlayers; i++)
        {
            players.Add(new Player($"Player {i}"));
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tm.inst.AdvanceTurn();
        }
    }
}