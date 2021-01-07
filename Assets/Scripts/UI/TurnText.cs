using TMPro;
using UnityEngine;
using tm = TurnManager;

[RequireComponent(typeof(TMP_Text))]
public class TurnText : MonoBehaviour
{
    private TMP_Text _text;

    // Start is called before the first frame update
    private void Start()
    {
        _text = GetComponent<TMP_Text>();
        _text.text = "Turn: 0";
        tm.OnNextTurn += () =>
            _text.text = $"Turn: {tm.turn}";
    }
}