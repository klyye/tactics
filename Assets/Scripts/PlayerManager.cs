using UnityEngine;
using tm = TurnManager;
using gm = GameManager;
using Rand = UnityEngine.Random;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] private string player0Name;
    [SerializeField] private string player1Name;

    /// <summary>
    ///     Janky placeholder. TODO
    /// </summary>
    [SerializeField] private Selectable[] player0Units;

    [SerializeField] private Selectable[] player1Units;

    // Start is called before the first frame update
    private void Start()
    {
        var player0 = new Player(player0Name);
        foreach (var unit in player0Units)
        {
            var spawnPoint = gm.grid.CoordToPosition(new Vector2Int(Rand.Range(0, gm.grid.width),
                Rand.Range(0, gm.grid.height)));
            var spawned = Instantiate(unit, spawnPoint, Quaternion.identity);
            player0.units.Add(spawned);
        }

        tm.AddPlayer(player0, 0);

        var player1 = new Player(player1Name);
        foreach (var unit in player1Units)
        {
            var spawnPoint = gm.grid.CoordToPosition(new Vector2Int(Rand.Range(0, gm.grid.width),
                Rand.Range(0, gm.grid.height)));
            var spawned = Instantiate(unit, spawnPoint, Quaternion.identity);
            player1.units.Add(spawned);
        }

        tm.AddPlayer(player1, 1);
    }
}