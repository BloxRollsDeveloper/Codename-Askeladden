using UnityEngine;

[RequireComponent(typeof(PlayerInput), typeof(PlayerMove), typeof(PlayerAttack))]
[RequireComponent(typeof(PlayerHealth))]
public class PlayerController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerMove _playerMove;
    private PlayerAttack _playerAttack;
    private PlayerHealth _playerHealth;

    private void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerMove = GetComponent<PlayerMove>();
        _playerAttack = GetComponent<PlayerAttack>();
        _playerHealth = GetComponent<PlayerHealth>();
    }

    private void Update()
    {
        _playerAttack.UpdateAttack(_playerInput.Attack);
        _playerHealth.TakeDamage(2f, _playerInput.SelfHarm);
    }

    private void FixedUpdate()
    {
        _playerMove.UpdateMovement(_playerInput.Movement, false);
    }
}
