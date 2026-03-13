using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    // Note to self, this script could also be used for a movement dash thing.
    // Also, if we add mud and other hazards, we could add things that decrease speed.
    
    [Header("Movement")]
    private PlayerDirection _playerDirection;
    private Rigidbody2D _rigidbody2D;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Animator animator;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerDirection = GetComponent<PlayerDirection>();
        
        UpdateIdleDirection(_playerDirection);
    }

    private void Update()
    {
        if (_rigidbody2D.linearVelocity == Vector2.zero)
        {
            UpdateIdleDirection(_playerDirection);
        }
        else
        {
            UpdateMoveDirection(_playerDirection);
        }
    }

    public void UpdateMovement(Vector2 movement, bool isDialogueActive)
    {
        if (isDialogueActive) return;
        _rigidbody2D.linearVelocity = movement * (moveSpeed * Time.deltaTime);
    }

    private void UpdateIdleDirection(PlayerDirection playerDirection)
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

    private void UpdateMoveDirection(PlayerDirection playerDirection)
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
