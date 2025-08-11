using System;
using UnityEngine;

public class PlayerInput
{
    public Action<Direction> _OnSwipeInput;
    Vector2 start;
    Vector2 end;
    Vector2 rawInput;
    Vector3 moveDirection;

    public void GetSwipeInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            SwipeStart();
        }
        if (Input.GetMouseButtonUp(0))
        {
            SwipeEnd();
        }
    }

    void SwipeStart()
    {
        start = Input.mousePosition;
    }

    void SwipeEnd()
    {
        end = Input.mousePosition;
        ProcessSwipeInput();
    }

    void ProcessSwipeInput()
    {
        rawInput = start - end;

        if (rawInput.magnitude < 50)
        {
            moveDirection = Vector3.zero;
            return;
        }
        if (Mathf.Abs(rawInput.x) >= Mathf.Abs(rawInput.y))
            rawInput.y = 0;
        else
            rawInput.x = 0;

        moveDirection = new Vector3(rawInput.x, 0, rawInput.y).normalized;
        _OnSwipeInput?.Invoke(GetDirection(moveDirection));
    }

    Direction GetDirection(Vector3 moveDirection)
    {
        if (moveDirection == Vector3.zero) return Direction.NONE;

        if (moveDirection.x < 0) return Direction.RIGHT;
        if (moveDirection.x > 0) return Direction.LEFT;
        if (moveDirection.z < 0) return Direction.FORWARD;
        if (moveDirection.z > 0) return Direction.BACK;

        return Direction.NONE;
    }
}