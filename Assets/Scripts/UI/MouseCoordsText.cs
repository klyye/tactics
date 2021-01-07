using TMPro;
using UnityEngine;
using gm = GameManager;

[RequireComponent(typeof(TMP_Text))]
public class MouseCoordsText : MonoBehaviour
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
        var mouseCoords = gm.grid.PositionToCoord(gm.cam.ScreenToWorldPoint(Input.mousePosition));
        mouseCoords.Clamp(Vector2Int.zero, new Vector2Int(gm.grid.width - 1, gm.grid.height - 1));
        _text.text = $"{mouseCoords}";
    }
}