using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] Transform skinTf;

    Stack<GameObject> bricks;
    bool isMoving;

    void Start()
    {

    }

    void Update()
    {

    }

    public void OnInit()
    {

    }

    public void OnDespawn()
    {

    }

    public void GetInput()
    {

    }

    public void Move()
    {

    }

    public void AddBrick()
    {

    }

    public void RemoveBrick()
    {

    }
}

public enum Direction
{
    NONE,
    FORWARD,
    BACK,
    LEFT,
    RIGHT
}