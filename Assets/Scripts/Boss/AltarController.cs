using UnityEngine;
using System;

public class AltarController : MonoBehaviour
{
    public static event Action OnAltarActivated;
    
    [Header("Altar Settings")]
    public float activationTime = 4f;

    private float progress = 0f;
    private bool isActivated = false;
    private bool playerInRange = false;
    
    private Animator animator;
    private InputSystem_Actions inputActions;

    void Start()
    {
        animator = GetComponent<Animator>();
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
    }

    void Update()
    {
        if (isActivated) return;
        
        if (playerInRange && inputActions.Player.Interact.IsPressed())
        {
            progress += Time.deltaTime;
            progress = Mathf.Min(progress, activationTime); // Updates progress
            UpdateAnimation();
        }
        
        if (progress >= activationTime)
            Activate();
    }

    void OnDestroy()
    {
        inputActions.Disable();
    }

    void UpdateAnimation()
    {
        animator.SetFloat("Progress", progress);
    }
    
    public void Activate()
    {
        if (isActivated) return;
        isActivated = true;
        OnAltarActivated?.Invoke();
        // Shines beam of light on the boss
    }

    public void Reset() // Deactivates the Altar
    {
        progress = 0f;
        isActivated = false;
        animator.SetFloat("Progress", 0f);
    }
    
    public void InterruptActivation() // Function to stop altar progress when player gets damaged
    {
        playerInRange = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }
}