using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Visual Cue")]
    [SerializeField] private GameObject visualCue;
    
    [Header("Ink JSON")]
    [SerializeField] private TextAsset inkJson;
    
    private bool playerInRange;
    private PlayerInputActions inputActions;
    
    private void Awake()
    {
        playerInRange = false;
        visualCue.SetActive(false);

        inputActions = new PlayerInputActions();
        inputActions.Enable();
    }

    private void Update()
    {
        visualCue.SetActive(playerInRange); // Shows Visual Cue

        if (playerInRange && inputActions.Player.Interact.WasPressedThisFrame())
        {
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
            playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
            playerInRange = false;
    }

    private void OnDestroy()
    {
        inputActions.Disable();
    }
}