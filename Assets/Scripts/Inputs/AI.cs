using System.Collections;
using System.Linq;
using UnityEngine;
using gm = GameManager;
using tm = TurnManager;
using pm = PlayerManager;
using Rand = UnityEngine.Random;

public class AI : MonoBehaviour
{
    private const float MAX_DECISION_TIME = 10;

    /// <summary>
    ///     lol this project is just me trying to fight unity using the static keyword
    /// </summary>
    private static AI inst;

    private static bool _thinking;

    private void Awake()
    {
        inst = this;
        _thinking = false;
    }

    public static void PlaceUnit()
    {
        var spawnPos = new Vector2Int(Rand.Range(0, gm.grid.width), Rand.Range(0, gm.grid.height));
        pm.SpawnAndAddNext(spawnPos);
    }

    public static void PlayTurn()
    {
        inst.Play();
    }

    /// <summary>
    ///     wow great function
    /// </summary>
    private void Play()
    {
        if (!_thinking)
            StartCoroutine(Think());
    }

    private static IEnumerator Think()
    {
        _thinking = true;
        var currTimer = 0.0f;
        // TODO just wanna see if this works
        var player = tm.currentPlayer;
        foreach (var actor in player.units)
        {
            var atker = actor.GetComponent<Attacker>();
            var mover = actor.GetComponent<Mover>();
            var otherPlayer = pm.players.Find(p => p != player);
            while (actor.actionPoints > 5)
            {
                var pos = gm.grid.PositionToCoord(actor.transform.position);
                if (atker)
                    foreach (var enemy in otherPlayer.units)
                    {
                        //todo: try to reduce this GetComponent stuff
                        ActionIssuer.IssueAttack(actor, enemy.GetComponent<Selectable>());
                    }

                if (mover)
                {
                    var reachable = Pathfinder.ReachablePoints(pos, actor.actionPoints);
                    var dest = reachable.OrderByDescending(c => c.ManhattanDist(pos)).First();
                    ActionIssuer.IssueMove(actor, dest);
                }

                yield return null;
                currTimer += Time.deltaTime;
                if (currTimer > MAX_DECISION_TIME) break;
            }
        }

        _thinking = false;
        tm.AdvanceTurn();
    }
}