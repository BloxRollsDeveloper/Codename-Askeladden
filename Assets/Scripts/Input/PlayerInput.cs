using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private InputSystem_Actions _inputSystem;

    public Vector2 Movement { get; private set; }
    public bool Attack  { get; private set; }
    public bool Interact  { get; private set; }
    
    private void Awake() => _inputSystem = new InputSystem_Actions();
    private void OnEnable() => _inputSystem.Enable();
    private void OnDisable() => _inputSystem.Disable();

    private void Update()
    {
        Movement = _inputSystem.Player.Move.ReadValue<Vector2>();

        Attack = _inputSystem.Player.Attack.WasPressedThisFrame();
        Interact = _inputSystem.Player.Interact.WasPressedThisFrame();
    }
    
}
