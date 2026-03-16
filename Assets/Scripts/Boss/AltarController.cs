using UnityEngine;
using UnityEngine.VFX;
using System;

public class AltarController : MonoBehaviour
{
    public static event Action OnAltarActivated;
    
    [Header("Altar Settings")]
    public float activationTime = 4f;
    
    [Header("VFX")]
    [SerializeField] private VisualEffect altarVFX;
    

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
        altarVFX.Stop();
    }

    void Update()
    {
        if (isActivated) return;

        bool isCharging = playerInRange && inputActions.Player.Interact.IsPressed();
        if (isCharging)
        {
            progress += Time.deltaTime;
            progress = Mathf.Min(progress, activationTime); // Updates progress
            UpdateAnimation();
            altarVFX.Play();
        }
        else
        {
            altarVFX.Stop();
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
        altarVFX.Stop();
        altarVFX.SendEvent("Burst");
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