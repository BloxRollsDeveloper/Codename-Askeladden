using UnityEngine;

[RequireComponent(typeof(InputManager))]
public class PlayerController : MonoBehaviour
{
    private InputManager _inputManager;
    private Rigidbody2D _rigidbody;
    
    [SerializeField] private float moveSpeed = 2f;

    private void Start()
    {
        _inputManager = GetComponent<InputManager>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (_inputManager.Interact)
        {
            // here you can put the start of your NPC interaction, David.
            print("Interact");
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocityX = _inputManager.MoveDirection.x * moveSpeed;
        _rigidbody.linearVelocityY = _inputManager.MoveDirection.y * moveSpeed;
    }
}
