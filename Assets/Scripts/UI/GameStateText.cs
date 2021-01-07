using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using pm = PlayerManager;
using gm = GameManager;

[RequireComponent(typeof(TMP_Text))]
public class GameStateText : MonoBehaviour
{
    private TMP_Text _text;

    // Start is called before the first frame update
    void Start()
    {
        _text = GetComponent<TMP_Text>();
        pm.OnAllUnitsPlaced += () => gameObject.SetActive(false);
        pm.OnOnePlayerRemaining += () =>
        {
            gameObject.SetActive(true);
            _text.text = $"Game Over, {pm.winner.name} wins";
        };
    }

    // Update is called once per frame
    void Update()
    {
        if (gm.state == gm.GameState.PRE)
            _text.text = $"{pm.currPlacingPlayer.name}, place your {pm.unitToSpawn.name}";
    }
}