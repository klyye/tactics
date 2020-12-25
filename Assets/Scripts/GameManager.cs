using UnityEngine;

/// <summary>
///     ??? does anything that i want it to.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static Camera cam;
    public static LevelGrid levelGrid;
    public static TurnManager turnManager;
    public Unit tempunit;

    // Start is called before the first frame update
    private void Awake()
    {
        cam = Camera.main;
        levelGrid = FindObjectOfType<LevelGrid>();
        turnManager = TurnManager.inst;
        tempunit = FindObjectOfType<Unit>();
    }

    // Update is called once per frame
    private void Update()
    {
        // TODO: placeholder. get rid of this
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // issue a move command to the turn manager. then advance the turn.
            turnManager.EnqueueAction(() => tempunit.Move(Vector2Int.zero, new Vector2Int(17, 9)));
            turnManager.AdvanceTurn();
        }
    }
}