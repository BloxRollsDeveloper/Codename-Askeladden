using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(PlayerMove))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerMove _playerMove;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerMove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        _playerMove.UpdateMovement(_playerInput.Movement, false);
    }
}
