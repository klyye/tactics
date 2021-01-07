using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using tm = TurnManager;
using gm = GameManager;
using Rand = UnityEngine.Random;

public class PlayerManager : MonoBehaviour
{
    public static List<Player> players;

    private static int _currSpawningUnit;

    /// <summary>
    ///     i can't set static fields in the unity inspector omegalul
    /// </summary>
    private static int unitsPerPlayer;

    private static IEnumerator<Selectable> _unitToSpawn;

    /// <summary>
    ///     Null if there is no winner, otherwise the current winner.
    /// </summary>
    public static Player winner;

    [SerializeField] private string[] _playerNames;

    /// <summary>
    ///     The amount of units each player can have.
    ///     todo maybe i can change this to a different amount per player later?
    /// </summary>
    [SerializeField] private int _unitsPerPlayer;

    /// <summary>
    ///     Janky placeholder. TODO
    /// </summary>
    [SerializeField] private Selectable[] _playerUnits;

    public static Selectable unitToSpawn => _unitToSpawn.Current;

    /// <summary>
    ///     The player that is currently placing units, pre-game.
    /// </summary>
    public static Player currPlacingPlayer => players[_currSpawningUnit / unitsPerPlayer];

    // Start is called before the first frame update
    private void Start()
    {
        if (_playerUnits.Length != _unitsPerPlayer * _playerNames.Length)
            throw new ArgumentException(
                "The amount of units in _playerUnits doesn't match the number in _unitsPerPlayer!");
        players = new List<Player>();
        _currSpawningUnit = 0;
        unitsPerPlayer = _unitsPerPlayer;
        _unitToSpawn = _playerUnits.AsEnumerable().GetEnumerator();
        _unitToSpawn.MoveNext();
        winner = null;
        for (var i = 0; i < _playerNames.Length; i++) players.Add(new Player(_playerNames[i]));
        tm.OnNextTurn += () =>
        {
            if (gm.state == GameManager.GameState.PLAYING && players.Count(p => p.alive) == 1)
            {
                winner = players.Find(p => p.alive);
                OnOnePlayerRemaining?.Invoke();
            }
        };
    }

    /// <summary>
    ///     Invokes when all pre-game units have been placed (game is gonna start).
    /// </summary>
    public static event Action OnAllUnitsPlaced;

    /// <summary>
    ///     Invokes when only one player is left (someone wins).
    /// </summary>
    public static event Action OnOnePlayerRemaining;

    /// <summary>
    ///     Spawns in the next to be spawned in the pre-game.
    /// </summary>
    /// <returns>Was the unit successfully spawned?</returns>
    public static void SpawnAndAddNext(Vector2Int spawnCoords)
    {
        if (gm.grid.IsOccupied(spawnCoords)) return;
        var spawnPoint = gm.grid.CoordToPosition(spawnCoords);
        if (!unitToSpawn) return;
        var spawned = Instantiate(unitToSpawn, spawnPoint, Quaternion.identity);
        spawned.name = unitToSpawn.name;
        _unitToSpawn.MoveNext();
        currPlacingPlayer.AddUnit(spawned);
        _currSpawningUnit++;
        if (_currSpawningUnit >= unitsPerPlayer * players.Count) OnAllUnitsPlaced?.Invoke();
    }
}