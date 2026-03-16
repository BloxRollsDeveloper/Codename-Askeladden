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
    
    public AnimationState animationState;
    
    private PlayerInput _playerInput;
    private PlayerMove _playerMove;
    private PlayerAttack _playerAttack;
    private PlayerHealth _playerHealth;
    private PlayerDirection _playerDirection;

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
        _playerDirection = GetComponent<PlayerDirection>();
        
        animationState = AnimationState.Idle;
    }

    private void Update()
    {
        _playerAttack.UpdateAttack(_playerInput.Attack);
        
        UpdateAnimationState();
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
            _knockbackTimer = 0f;
        }
        
    }

    private void UpdateAnimationState()
    {
        switch (animationState)
        {
            case AnimationState.Idle:
                _playerMove.UpdateIdleDirection(_playerDirection);
                break;
            case AnimationState.Run:
                _playerMove.UpdateMoveDirection(_playerDirection);
                break;
            case AnimationState.Attack:
                _playerAttack.UpdateAttackDirection(_playerDirection);
                break;
            case AnimationState.Damage:
                _playerHealth.UpdateDamage(_playerDirection);
                break;
            case AnimationState.Dead:
                _playerHealth.UpdateDeath(_playerDirection);
                break;
        }
    }
}
