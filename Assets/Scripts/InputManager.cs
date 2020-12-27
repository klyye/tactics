using UnityEngine;
using gm = GameManager;
using tm = TurnManager;

public class InputManager : MonoBehaviour
{
    private Selectable _selected;

    private void Start()
    {
        tm.inst.OnNextTurn += Deselect;
    }

    private void Deselect()
    {
        _selected = null;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _selected)
        {
            var mouseWorldPos = gm.cam.ScreenToWorldPoint(Input.mousePosition);
            var dest = gm.grid.PositionToCoord(mouseWorldPos);
            ActionIssuer.IssueAction(_selected, dest);
            Deselect();
        }
    }

    public void Select(Selectable selected)
    {
        if (!_selected)
        {
            _selected = selected;
            return;
        }

        // selecting another unit after a unit is already selected counts as an attack
        ActionIssuer.IssueAction(_selected, selected);
        Deselect();
    }
}