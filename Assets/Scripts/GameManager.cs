using UnityEngine;
using tm = TurnManager;

/// <summary>
///     ??? does anything that i want it to.
/// </summary>
public class GameManager : MonoBehaviour
{
    public static Camera cam;
    public static LevelGrid grid;
    public static InputManager inputMan;

    // Start is called before the first frame update
    private void Awake()
    {
        cam = Camera.main;
        inputMan = FindObjectOfType<InputManager>();
        grid = FindObjectOfType<LevelGrid>();
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