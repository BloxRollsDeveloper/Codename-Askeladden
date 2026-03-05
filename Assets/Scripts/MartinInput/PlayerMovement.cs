using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public float speed = 5;
    public Rigidbody2D rb;
    private PlayerAttack _playerAttack;
    
    private PlayerInputActions inputActions;

    void Awake() => inputActions = new PlayerInputActions();
    private void OnEnable() => inputActions.Enable();
    private void OnDisable() => inputActions.Disable();

    private void Start()
    {
        _playerAttack = GetComponent<PlayerAttack>();
    }

    void FixedUpdate()
    {
        if (DialogueManager.GetInstance().dialogueIsPlaying)
        {
            return;
        }
        Vector2 move = inputActions.Player.Move.ReadValue<Vector2>();
        rb.linearVelocity = move * speed;
    }

    private void Update()
    {
        if (inputActions.Player.Attack.WasPressedThisFrame())
        {
            print("Attack!");
            _playerAttack.UpdateAttack();
        }
    }
}