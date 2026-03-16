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
    private PlayerDirection _playerDirection;
    [SerializeField] private float moveSpeed = 5f;
    
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private float idleAnimTime;
    [SerializeField] private float runAnimTime;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerController = GetComponent<PlayerController>();
        _playerDirection = GetComponent<PlayerDirection>();
    }
    
    public void UpdateMovement(Vector2 movement, bool isDialogueActive)
    {
        if (isDialogueActive) return;
        _rigidbody2D.linearVelocity = movement * (moveSpeed * Time.deltaTime);

        if (Math.Abs(_rigidbody2D.linearVelocityX) > 0 || Math.Abs(_rigidbody2D.linearVelocityY) > 0)
        {
            // _playerController.animationState = PlayerController.AnimationState.Run;
            UpdateMoveDirection(_playerDirection);
        }
        else
        {
            // _playerController.animationState = PlayerController.AnimationState.Idle;
            UpdateIdleDirection(_playerDirection);
        }
    }

    public void UpdateIdleDirection(PlayerDirection playerDirection)
    {
        switch (playerDirection.direction)
        {
            case PlayerDirection.Direction.North:
                animator.Play(IdleNorth);
                break;
            case PlayerDirection.Direction.East:
                animator.Play(IdleEast);
                break;
            case PlayerDirection.Direction.South:
                animator.Play(IdleSouth);
                break;
            case PlayerDirection.Direction.West:
                animator.Play(IdleWest);
                break;
            case PlayerDirection.Direction.NorthEast:
                animator.Play(IdleNorthEast);
                break;
            case PlayerDirection.Direction.NorthWest:
                animator.Play(IdleNorthWest);
                break;
            case PlayerDirection.Direction.SouthEast:
                animator.Play(IdleSouthEast);
                break;
            case PlayerDirection.Direction.SouthWest:
                animator.Play(IdleSouthWest);
                break;
        }
    }

    public void UpdateMoveDirection(PlayerDirection playerDirection)
    {
        switch (playerDirection.direction)
        {
            case PlayerDirection.Direction.North:
                animator.Play(RunNorth);
                break;
            case PlayerDirection.Direction.East:
                animator.Play(RunEast);
                break;
            case PlayerDirection.Direction.South:
                animator.Play(RunSouth);
                break;
            case PlayerDirection.Direction.West:
                animator.Play(RunWest);
                break;
            case PlayerDirection.Direction.NorthEast:
                animator.Play(RunNorthEast);
                break;
            case PlayerDirection.Direction.NorthWest:
                animator.Play(RunNorthWest);
                break;
            case PlayerDirection.Direction.SouthEast:
                animator.Play(RunSouthEast);
                break;
            case PlayerDirection.Direction.SouthWest:
                animator.Play(RunSouthWest);
                break;
        }
    }
    
    // Idle Animations 
    private static readonly int IdleNorth = Animator.StringToHash("IdleNorth");
    private static readonly int IdleEast = Animator.StringToHash("IdleEast");
    private static readonly int IdleSouth = Animator.StringToHash("IdleSouth");
    private static readonly int IdleWest = Animator.StringToHash("IdleWest");
    private static readonly int IdleNorthEast = Animator.StringToHash("IdleNorthEast");
    private static readonly int IdleNorthWest = Animator.StringToHash("IdleNorthWest");
    private static readonly int IdleSouthEast = Animator.StringToHash("IdleSouthEast");
    private static readonly int IdleSouthWest = Animator.StringToHash("IdleSouthWest");
    
    // Run Animations 
    private static readonly int RunNorth = Animator.StringToHash("RunNorth");
    private static readonly int RunEast = Animator.StringToHash("RunEast");
    private static readonly int RunSouth = Animator.StringToHash("RunSouth");
    private static readonly int RunWest = Animator.StringToHash("RunWest");
    private static readonly int RunNorthEast = Animator.StringToHash("RunNorthEast");
    private static readonly int RunNorthWest = Animator.StringToHash("RunNorthWest");
    private static readonly int RunSouthEast = Animator.StringToHash("RunSouthEast");
    private static readonly int RunSouthWest = Animator.StringToHash("RunSouthWest");
}
