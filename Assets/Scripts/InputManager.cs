using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gm = GameManager;
using tm = TurnManager;

public class InputManager : MonoBehaviour
{
    private Mover _selected;
    private ISet<Mover> _issuedMovers;

    // Start is called before the first frame update
    void Start()
    {
        _issuedMovers = new HashSet<Mover>();
        tm.inst.OnNextTurn += _issuedMovers.Clear;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _selected)
        {
            var mouseWorldPos = gm.cam.ScreenToWorldPoint(Input.mousePosition);
            var dest = gm.grid.PositionToCoord(mouseWorldPos);
            var mover = _selected;
            var start = gm.grid.PositionToCoord(mover.transform.position);
            var path = gm.grid.ShortestPath(start, dest, mover.movePoints);
            if (path != null && !_issuedMovers.Contains(mover))
            {
                Debug.Log($"Issuing move {mover.name} to ({dest.x}, {dest.y})");
                mover.MoveAlong(path);
                _issuedMovers.Add(mover);
            }

            _selected = null;
        }
    }

    public void Select(Mover selected)
    {
        _selected = selected;
    }
}