using UnityEngine;

public class InputManager : MonoBehaviour
{
    private InputSystem_Actions _inputSystem;

    public Vector2 MoveDirection { get; private set; }
    public bool Interact { get; private set; } 
    public bool Attack { get; private set; }
    
    private void Awake() => _inputSystem = new InputSystem_Actions();
    private void OnEnable() => _inputSystem.Enable();
    private void OnDisable() => _inputSystem.Disable();

    private void Update()
    {
        MoveDirection = _inputSystem.Player.Move.ReadValue<Vector2>();

        Interact = _inputSystem.Player.Interact.WasPressedThisFrame();
        Attack = _inputSystem.Player.Attack.WasPressedThisFrame();
    }
}
