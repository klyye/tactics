using System;
using UnityEngine;
using gm = GameManager;
using tm = TurnManager;

/// <summary>
///     Turns user inputs into issued actions.
/// </summary>
public class InputManager : MonoBehaviour
{
    /// <summary>
    ///     The key to press to issue an attack.
    /// </summary>
    public KeyCode attackKey;

    /// <summary>
    ///     The key to press to issue a move.
    /// </summary>
    public KeyCode moveKey;

    /// <summary>
    ///     The currently selected Selectable.
    /// </summary>
    private static Selectable _selected;

    public static Selectable selected => _selected;

    /// <summary>
    ///     What action the player wants to issue.
    /// </summary>
    private static ActionState _state;

    private void Start()
    {
        tm.inst.OnNextTurn += Deselect;
        _state = ActionState.NONE;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _state == ActionState.MOVE)
        {
            var mouseWorldPos = gm.cam.ScreenToWorldPoint(Input.mousePosition);
            var dest = gm.grid.PositionToCoord(mouseWorldPos);
            ActionIssuer.IssueMove(_selected, dest);
            Deselect();
        }

        if (_selected)
        {
            if (Input.GetKeyDown(attackKey))
            {
                _state = ActionState.ATTACK;
                OnEnterAttackState?.Invoke();
            }
            else if (Input.GetKeyDown(moveKey))
            {
                _state = ActionState.MOVE;
                OnEnterMoveState?.Invoke();
            }
        }
    }

    /// <summary>
    ///     Triggers when the input moves into a state where no action is being performed.
    /// </summary>
    public static event Action OnEnterNoneState;

    /// <summary>
    ///     Triggers when the input moves into a state where a move is being performed.
    /// </summary>
    public static event Action OnEnterMoveState;

    /// <summary>
    ///     Triggers when the input moves into a state where an attack is being performed.
    /// </summary>
    public static event Action OnEnterAttackState;

    /// <summary>
    ///     Clear out the currently selected selectable.
    /// </summary>
    private static void Deselect()
    {
        _selected = null;
        OnEnterNoneState?.Invoke();
        _state = ActionState.NONE;
    }

    /// <summary>
    ///     Select a selectable.
    /// </summary>
    /// <param name="selected">The selectable to be selected.</param>
    public static void Select(Selectable selected)
    {
        if (_state == ActionState.ATTACK)
        {
            // selecting another unit after a unit is already selected counts as an attack
            ActionIssuer.IssueAttack(_selected, selected);
            Deselect();
        }
        else
        {
            _selected = selected;
        }
    }

    /// <summary>
    ///     All possible types of actions that the player can issue.
    /// </summary>
    private enum ActionState
    {
        NONE,
        MOVE,
        ATTACK
    }
}