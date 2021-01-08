using System;
using UnityEngine;
using gm = GameManager;
using tm = TurnManager;
using pm = PlayerManager;

/// <summary>
///     Turns user inputs into issued actions.
/// </summary>
public class InputManager : MonoBehaviour
{
    /// <summary>
    ///     What action the player wants to issue.
    /// </summary>
    private static ActionState _state;

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
    public static Selectable selected { get; private set; }

    private void Start()
    {
        tm.OnNextTurn += Deselect;
        _state = ActionState.NONE;
    }

    // Update is called once per frame
    private void Update()
    {
        switch (gm.state)
        {
            case gm.GameState.PLAYING:
                if (tm.currentPlayer.isAI)
                    AI.PlayTurn();
                else
                    HandleInputPlaying();
                break;
            case gm.GameState.PRE:
                if (tm.currentPlayer.isAI)
                    AI.PlaceUnit();
                else
                    HandleInputPre();
                break;
            case gm.GameState.POST:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void HandleInputPre()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            var mouseWorldPos = gm.cam.ScreenToWorldPoint(Input.mousePosition);
            var dest = gm.grid.PositionToCoord(mouseWorldPos);
            pm.SpawnAndAddNext(dest);
        }
    }

    private void HandleInputPlaying()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _state == ActionState.MOVE)
        {
            var mouseWorldPos = gm.cam.ScreenToWorldPoint(Input.mousePosition);
            var dest = gm.grid.PositionToCoord(mouseWorldPos);
            ActionIssuer.IssueMove(selected.actor, dest);
            Deselect();
        }

        if (selected)
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

        if (Input.GetKeyDown(KeyCode.Space)) tm.AdvanceTurn();
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
        selected = null;
        OnEnterNoneState?.Invoke();
        _state = ActionState.NONE;
    }

    /// <summary>
    ///     Select a selectable.
    /// </summary>
    /// <param name="clicked">The selectable to be selected.</param>
    public static void Select(Selectable clicked)
    {
        if (_state == ActionState.ATTACK)
        {
            // selecting another unit after a unit is already selected counts as an attack
            ActionIssuer.IssueAttack(selected.actor, clicked);
            Deselect();
        }
        else
        {
            selected = clicked;
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