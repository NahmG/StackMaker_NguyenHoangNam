using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Player : MonoBehaviour
{
    [SerializeField] Transform skinTf;
    public Transform SkinTF => skinTf;
    bool isWin;

    #region UNITY_API
    void Awake()
    {
        playerInput._OnSwipeInput += SetDirection;
    }

    void Update()
    {
        playerInput?.GetSwipeInput();

        Move();
    }

    void OnDestroy()
    {
        playerInput._OnSwipeInput -= SetDirection;
    }
    #endregion

    #region SETUP
    public void OnInit(Vector3 startPosition)
    {
        isWin = false;
        isMoving = false;
        transform.position = startPosition;
        nextPosition = transform.position;
        currentDirection = Direction.NONE;

        skinTf.gameObject.SetActive(true);
    }

    public void OnDespawn()
    {
        ClearBrick();
        skinTf.gameObject.SetActive(false);
    }
    #endregion

    #region MOVEMENT
    [Header("Movement")]
    [HideInInspector] public bool isPushBlock;
    [SerializeField] float speed;
    PlayerInput playerInput = new();
    Direction currentDirection;
    public Direction Direction => currentDirection;
    bool isMoving;
    Vector3 nextPosition;

    public void SetDirection(Direction direction)
    {
        if (GameManager.Ins.IsState(GameState.SETTING)) return;
        if (isMoving) return;

        currentDirection = direction;
        nextPosition = GetNextPoint(direction);
    }

    void Move()
    {
        if (Vector3.Distance(transform.position, nextPosition) < 0.01f)
        {
            isMoving = false;
            if (isPushBlock)
            {
                SetDirection(GetAvailableDirection());
            }
        }
        else
        {
            isMoving = true;
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
        }

    }

    public Vector3 GetNextPoint(Direction direction)
    {
        Vector3 checkDirection = DirectionToVector(direction);

        Vector3 currentPoint = transform.position;
        Vector3 checkPoint = currentPoint + checkDirection;

        int attemp = 0;

        while (Physics.Raycast(checkPoint, Vector3.down, 5f, LayerMask.GetMask(CONSTANTS.LAYER_BRICK)) && attemp <= 10000)
        {
            currentPoint = checkPoint;
            checkPoint += checkDirection;

            attemp++;
        }

        return currentPoint;
    }

    public Direction GetAvailableDirection()
    {
        Direction opposite = GetOppositeDirection(currentDirection);

        foreach (Direction dir in Enum.GetValues(typeof(Direction)))
        {
            if (dir == opposite) continue;

            if (!IsBlocked(dir))
            {
                return dir; // return a not opposite or not blocked direction
            }
        }

        return Direction.NONE;
    }

    Direction GetOppositeDirection(Direction dir)
    {
        return dir switch
        {
            Direction.FORWARD => Direction.BACK,
            Direction.BACK => Direction.FORWARD,
            Direction.LEFT => Direction.RIGHT,
            Direction.RIGHT => Direction.LEFT,
            _ => dir,
        };
    }

    bool IsBlocked(Direction direction)
    {
        return Vector3.Distance(transform.position, GetNextPoint(direction)) < 0.1f;
    }

    Vector3 DirectionToVector(Direction dir)
    {
        switch (dir)
        {
            case Direction.FORWARD:
                return Vector3.forward;
            case Direction.BACK:
                return Vector3.back;
            case Direction.LEFT:
                return Vector3.left;
            case Direction.RIGHT:
                return Vector3.right;
            default:
                return Vector3.zero;
        }
    }
    #endregion

    #region BRICK

    [Header("Brick")]
    [SerializeField] GameObject brickPref;
    [SerializeField] Transform brickHolder;
    [SerializeField] float brickHeight;
    public Stack<GameObject> playerBricks { get; private set; } = new();
    public void AddBrick()
    {
        int index = playerBricks.Count;

        skinTf.localPosition += new Vector3(0, brickHeight, 0);

        GameObject newBrick = Instantiate(brickPref, brickHolder);
        newBrick.transform.localPosition = brickHeight * index * Vector3.up;

        playerBricks.Push(newBrick);
    }

    public void RemoveBrick()
    {
        skinTf.localPosition -= new Vector3(0, brickHeight, 0);
        Destroy(playerBricks.Pop());
    }

    public void ClearBrick()
    {
        while (playerBricks.Count > 0)
        {
            skinTf.position -= new Vector3(0, brickHeight, 0);
            Destroy(playerBricks.Pop());
        }
    }

    #endregion

    #region CHECK_WIN
    public void OnWin()
    {
        isWin = true;
        GameplayManager.Ins.brickCount = playerBricks.Count;
        GameplayManager.Ins.Win();
        OnDespawn();
    }
    #endregion
}

