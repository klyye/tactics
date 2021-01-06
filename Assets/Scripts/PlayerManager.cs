using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using tm = TurnManager;
using gm = GameManager;
using Rand = UnityEngine.Random;

public class PlayerManager : MonoBehaviour
{
    public static Player[] players;

    private static int _currSpawningUnit;

    /// <summary>
    ///     i can't set static fields in the unity inspector omegalul
    /// </summary>
    private static int unitsPerPlayer;

    private static IEnumerator<Selectable> _unitToSpawn;

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

    // Start is called before the first frame update
    private void Start()
    {
        if (_playerUnits.Length != _unitsPerPlayer * _playerNames.Length)
            throw new ArgumentException(
                "The amount of units in _playerUnits doesn't match the number in _unitsPerPlayer!");
        players = new Player[_playerNames.Length];
        _currSpawningUnit = 0;
        unitsPerPlayer = _unitsPerPlayer;
        _unitToSpawn = _playerUnits.AsEnumerable().GetEnumerator();
        for (var i = 0; i < _playerNames.Length; i++) players[i] = new Player(_playerNames[i]);
    }

    public static event Action OnAllUnitsPlaced;

    /// <summary>
    ///     Spawns in the next to be spawned in the pre-game.
    /// </summary>
    /// <returns>Was the unit successfully spawned?</returns>
    public static void SpawnAndAddNext(Vector2Int spawnCoords)
    {
        var spawnPoint = gm.grid.CoordToPosition(spawnCoords);
        _unitToSpawn.MoveNext();
        var unit = _unitToSpawn.Current;
        if (!unit) return;
        var spawned = Instantiate(unit, spawnPoint, Quaternion.identity);
        players[_currSpawningUnit / unitsPerPlayer].units.Add(spawned);
        _currSpawningUnit++;
        if (_currSpawningUnit >= unitsPerPlayer * players.Length) OnAllUnitsPlaced?.Invoke();
    }
}