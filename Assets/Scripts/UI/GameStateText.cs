using System;
using TMPro;
using UnityEngine;
using pm = PlayerManager;
using gm = GameManager;
using im = InputManager;
using tm = TurnManager;

[RequireComponent(typeof(TMP_Text))]
public class GameStateText : MonoBehaviour
{
    private TMP_Text _text;

    // Start is called before the first frame update
    private void Start()
    {
        _text = GetComponent<TMP_Text>();
    }

    // Update is called once per frame
    private void Update()
    {
        // checking for stuff every frame: is this better/worse than an event based system?
        switch (gm.state)
        {
            case GameManager.GameState.PRE:
                _text.text = $"{pm.currPlacingPlayer.name}, place your {pm.unitToSpawn.name}";
                break;
            case GameManager.GameState.PLAYING:
                var sel = im.selected;
                if (sel)
                    _text.text = $"{sel.name} has {sel.actor.actionPoints} action points left";
                else
                    _text.text = tm.currentPlayer.isAI
                        ? "AI thinking..."
                        : $"{tm.currentPlayer.name}, click to select a unit";
                break;
            case GameManager.GameState.POST:
                _text.text = $"Game Over, {pm.winner.name} wins";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }
}