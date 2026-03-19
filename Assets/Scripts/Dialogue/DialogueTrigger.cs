using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJson;
    
    private bool playerInRange;
    private InputSystem_Actions inputActions;
    
    private Collider2D playerCollider;
    
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);

        inputActions = new InputSystem_Actions();
        inputActions.Enable();
    }

    private void Update()
    {
        bool dialoguePlaying = DialogueManager.GetInstance().dialogueIsPlaying;
        
        visualCue.SetActive(playerInRange && !dialoguePlaying); // Displays the Visual Cue
        
        if (playerInRange && !dialoguePlaying && inputActions.Player.Interact.WasPressedThisFrame()) // Interact Pressed
        {
            playerCollider.TryGetComponent(out PlayerMove playerMove);
            playerMove.isDialogueActive = true;
            inputActions.Player.Disable();
            TriggerDialogue();
        }
    }

    private void TriggerDialogue() // Triggers the Dialogue
    {
        DialogueManager.GetInstance().EnterDialogueMode(inkJson);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = true;
            playerCollider = collider;
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            playerCollider = null;
        }
    }

    private void OnDestroy()
    {
        inputActions.Disable();
    }
}