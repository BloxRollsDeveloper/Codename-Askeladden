using UnityEngine;
using System;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    // Note to self, this script could also be used for a movement dash thing.
    // Also, if we add mud and other hazards, we could add things that decrease speed.
    
    [Header("Movement")]
    private Rigidbody2D _rigidbody2D;
    private PlayerController _playerController;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator animator;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();
    }
    
    public void UpdateMovement(Vector2 movement, bool isDialogueActive)
    {
        if (isDialogueActive) return;
        _rigidbody2D.linearVelocity = movement * (moveSpeed * Time.deltaTime);

        if (Math.Abs(_rigidbody2D.linearVelocityX) > 0 || Math.Abs(_rigidbody2D.linearVelocityY) > 0)
        {
            _playerController.animationState = PlayerController.AnimationState.Run;
        }
        else
        {
            _playerController.animationState = PlayerController.AnimationState.Idle;
        }
    }

    public void UpdateIdleDirection(PlayerDirection playerDirection)
    {
        switch (playerDirection.direction)
        {
            case PlayerDirection.Direction.North:
                animator.Play("IdleNorth");
                break;
            case PlayerDirection.Direction.East:
                animator.Play("IdleEast");
                break;
            case PlayerDirection.Direction.South:
                animator.Play("IdleSouth");
                break;
            case PlayerDirection.Direction.West:
                animator.Play("IdleWest");
                break;
            case PlayerDirection.Direction.NorthEast:
                animator.Play("IdleNorthEast");
                break;
            case PlayerDirection.Direction.NorthWest:
                animator.Play("IdleNorthWest");
                break;
            case PlayerDirection.Direction.SouthEast:
                animator.Play("IdleSouthEast");
                break;
            case PlayerDirection.Direction.SouthWest:
                animator.Play("IdleSouthWest");
                break;
        }
    }

    public void UpdateMoveDirection(PlayerDirection playerDirection)
    {
        switch (playerDirection.direction)
        {
            case PlayerDirection.Direction.North:
                animator.Play("RunNorth");
                break;
            case PlayerDirection.Direction.East:
                animator.Play("RunEast");
                break;
            case PlayerDirection.Direction.South:
                animator.Play("RunSouth");
                break;
            case PlayerDirection.Direction.West:
                animator.Play("RunWest");
                break;
            case PlayerDirection.Direction.NorthEast:
                animator.Play("RunNorthEast");
                break;
            case PlayerDirection.Direction.NorthWest:
                animator.Play("RunNorthWest");
                break;
            case PlayerDirection.Direction.SouthEast:
                animator.Play("RunSouthEast");
                break;
            case PlayerDirection.Direction.SouthWest:
                animator.Play("RunSouthWest");
                break;
        }
    }
}
