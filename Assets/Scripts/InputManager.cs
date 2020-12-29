using UnityEngine;
using gm = GameManager;
using tm = TurnManager;

/// <summary>
///     Turns user inputs into issued actions.
/// </summary>
public class InputManager : MonoBehaviour
{
    /// <summary>
    ///     The currently selected Selectable.
    /// </summary>
    private Selectable _selected;

    private void Start()
    {
        tm.inst.OnNextTurn += Deselect;
    }

    /// <summary>
    ///     Clear out the currently selected selectable.
    /// </summary>
    private void Deselect()
    {
        _selected = null;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && _selected)
        {
            var mouseWorldPos = gm.cam.ScreenToWorldPoint(Input.mousePosition);
            var dest = gm.grid.PositionToCoord(mouseWorldPos);
            ActionIssuer.IssueAction(_selected, dest);
            Deselect();
        }
    }

    /// <summary>
    ///     Select a selectable.
    /// </summary>
    /// <param name="selected">The selectable to be selected.</param>
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