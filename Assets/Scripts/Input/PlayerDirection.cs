using UnityEngine;
using System;

public class PlayerDirection : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private bool isMoving;
    
    public enum Direction
    {
        North,
        East,
        South,
        West,
        NorthEast,
        NorthWest,
        SouthEast,
        SouthWest
    }
    
    public Direction direction;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        ChangeDirection();
    }

    private void ChangeDirection()
    {
        // in this function, we will figure out a way to change directions.
        Vector2 currentDirection = new Vector2(_rigidbody2D.linearVelocityX, _rigidbody2D.linearVelocityY);

        if (currentDirection is { x: 0, y: > 0 })
        {
            direction = Direction.North;
        }

        if (currentDirection is { x: > 0, y: 0 })
        {
            direction = Direction.East;
        }

        if (currentDirection is { x: 0, y: < 0 })
        {
            direction = Direction.South;
        }

        if (currentDirection is { x: < 0, y: 0 })
        {
            direction = Direction.West;
        }

        if (currentDirection is { x: > 0, y: > 0 })
        {
            direction = Direction.NorthEast;
        }

        if (currentDirection is { x: < 0, y: > 0 })
        {
            direction = Direction.NorthWest;
        }

        if (currentDirection is { x: > 0, y: < 0 })
        {
            direction = Direction.SouthEast;
        }

        if (currentDirection is { x: < 0, y: < 0 })
        {
            direction = Direction.SouthWest;
        }
    }
}
