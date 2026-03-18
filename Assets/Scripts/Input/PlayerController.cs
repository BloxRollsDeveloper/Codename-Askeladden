using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(PlayerMove), typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerHealth), typeof(PlayerDirection))]
public class PlayerController : MonoBehaviour
{
    // basically, we need a way to play animations, without them overlapping each other.
    public enum AnimationState
    {
        Idle,
        Run,
        Attack,
        Damage,
        Dead,
        Fiddle
    }
    
    [Header("Animation")]
    public AnimationState animationState;
    public bool isMoving;
    public bool isAttacking;
    public bool isTakingDamage;
    public bool isDead;
    
    
    private PlayerInput _playerInput;
    private PlayerMove _playerMove;
    private PlayerAttack _playerAttack;
    private PlayerHealth _playerHealth;
    private PlayerAnimationController _playerAnimationController;

    [Header("Knockback")] 
    [SerializeField] private float knockbackDelay = 0.2f; 
    private float _knockbackTimer = 0f;
    public bool isKnockedBack = false;
    
    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerMove = GetComponent<PlayerMove>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerAnimationController = GetComponentInChildren<PlayerAnimationController>();
    }

    private void Update()
    {
        _playerAttack.UpdateAttack(_playerInput.Attack);
        _playerAnimationController.UpdateAnimation(_playerInput.Attack, false, false);
    }

    private void FixedUpdate()
    {
        if (isKnockedBack && _knockbackTimer <= knockbackDelay)
        {
            _knockbackTimer += Time.fixedDeltaTime;
            isKnockedBack = false;
        }
        else
        {
            _playerMove.UpdateMovement(_playerInput.Movement, false);
            _playerAnimationController.UpdateMoveDirection(_playerInput.Movement);
            _knockbackTimer = 0f;
        }
        
    }
}
