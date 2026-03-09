using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMove : MonoBehaviour
{
    // Note to self, this script could also be used for a movement dash thing.
    // Also, if we add mud and other hazards, we could add things that decrease speed.
    
    private Rigidbody2D _rigidbody2D;

    [SerializeField] private float moveSpeed = 2f;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void UpdateMovement(Vector2 movement, bool isDialogueActive)
    {
        if (isDialogueActive) return;
        _rigidbody2D.linearVelocity = (movement * moveSpeed).normalized;
    }
}
