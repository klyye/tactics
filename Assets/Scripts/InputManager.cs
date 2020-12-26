using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using gm = GameManager;
using tm = TurnManager;

public class InputManager : MonoBehaviour
{
    private bool _movableSelected;
    private Mover _selected;

    // Start is called before the first frame update
    void Start()
    {
        _movableSelected = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && _movableSelected)
        {
            var mouseWorldPos = gm.cam.ScreenToWorldPoint(Input.mousePosition);
            var dest = gm.grid.PositionToCoord(mouseWorldPos);
            var mover = _selected;
            var start = gm.grid.PositionToCoord(mover.transform.position);
            Debug.Log($"Issuing move {mover.name} to ({dest.x}, {dest.y})");
            var path = gm.grid.ShortestPath(start, dest, mover.movePoints);
            if (path != null)
                tm.inst.IssueCommand(() => mover.MoveAlong(path));
            _movableSelected = false;
            _selected = null;
        }
    }

    public void SelectMovable(Mover selected)
    {
        _selected = selected;
        _movableSelected = true;
    }
}