using UnityEngine;
using UnityEngine.VFX;
using System;

public class AltarController : MonoBehaviour
{
    public static event Action OnAltarActivated;
    
    [Header("Altar Settings")]
    public float activationTime = 4f;
    
    [Header("Materials")]
    [SerializeField] private SpriteRenderer outlineRenderer;
    private SpriteRenderer altarRenderer;
    
    [Header("VFX")]
    [SerializeField] private VisualEffect altarVFX;
    

    private float progress = 0f;
    private bool isActivated = false;
    private bool playerInRange = false;
    private bool isCharging = false;
    
    private Animator animator;
    private InputSystem_Actions inputActions;

    void Start()
    {
        animator = GetComponent<Animator>();
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
        altarRenderer = GetComponent<SpriteRenderer>();
        outlineRenderer.enabled = false;
        altarVFX.Stop();
    }

    void Update()
    {
        outlineRenderer.sprite = altarRenderer.sprite;
        if (isActivated) return;

        bool holding = playerInRange && inputActions.Player.Interact.IsPressed();

        if (holding && !isCharging)
        {
            isCharging = true;
            altarVFX.Play();
        }
        else if (!holding && isCharging)
        {
            isCharging = false;
            altarVFX.Stop();
        }

        if (holding)
        {
            progress += Time.deltaTime;
            progress = Mathf.Min(progress, activationTime);
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
        isCharging = false;
        altarVFX.Stop();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            outlineRenderer.enabled = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            outlineRenderer.enabled = false;
        }
    }
}