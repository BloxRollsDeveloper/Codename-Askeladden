using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(PlayerMove), typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerMove _playerMove;
    private PlayerAttack _playerAttack;
    private PlayerHealth _playerHealth;
    private PlayerAnimationController _playerAnimationController;

    [Header("Knockback")] 
    [SerializeField] private float knockbackDelay = 0.2f; 
    private float _knockbackTimer = 0f;
    public bool isKnockedBack = false;
    
    private DialogueManager _dialogueManager;
    
    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerMove = GetComponent<PlayerMove>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerHealth = GetComponent<PlayerHealth>();
        _playerAnimationController = GetComponentInChildren<PlayerAnimationController>();

        _dialogueManager = DialogueManager.GetInstance();
    }

    private void Update()
    {
        if (_dialogueManager.dialogueIsPlaying) return;
        
        _playerAttack.UpdateAttack(_playerInput.Attack);
        _playerAnimationController.UpdateAnimation(_playerInput.Attack, false, false, false);
    }

    private void FixedUpdate()
    {
        if (_dialogueManager.dialogueIsPlaying) return;
        
        if (isKnockedBack && _knockbackTimer <= knockbackDelay)
        {
            _knockbackTimer += Time.fixedDeltaTime;
            isKnockedBack = false;
        }
        else
        {
            _playerMove.UpdateMovement(_playerInput.Movement);
            _playerAnimationController.UpdateMoveDirection(_playerInput.Movement); 
            _knockbackTimer = 0f;
        }
    }
}
