using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rigidbody;
    [SerializeField] private float moveSpeed = 2f;

    public void UpdateMovement(Vector2 moveDirection)
    {
        _rigidbody.linearVelocityX = moveDirection.x * moveSpeed * Time.deltaTime;
        _rigidbody.linearVelocityY = moveDirection.y * moveSpeed * Time.deltaTime;
    }
}
