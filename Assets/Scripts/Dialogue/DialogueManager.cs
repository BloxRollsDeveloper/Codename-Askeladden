using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour
{
    [Header("Typing Speed")]
    [SerializeField] private float typingSpeed = 0.04f;
    
    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private TextMeshProUGUI displayNameText;
    [SerializeField] private Animator portraitAnimator;
    [SerializeField] private GameObject continueIcon;
    private Animator LayoutAnimator;
    
    [Header("Choices UI")]
    [SerializeField] private GameObject[] choices;
    private TextMeshProUGUI[] choicesText;
    
    private Story currentStory;
    private int currentChoiceIndex = 0;
    private bool canContinueToNextLine = false;
    private bool submitConsumed = false;

    public bool dialogueIsPlaying { get; private set; }
    
    private Coroutine displayLineCoroutine;
    
    
    private static DialogueManager instance;
    
    // TAGS
    private const string SPEAKER_TAG = "speaker";
    
    private const string PORTRAIT_TAG = "portrait";
    
    private const string LAYOUT_TAG = "layout";
    
    
    private InputSystem_Actions inputActions;

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Found more than one DialogueManagers in the scene");
        }
        instance = this;
        
        inputActions = new InputSystem_Actions();
        inputActions.Enable();
    }

    private void OnEnable()
    {
        inputActions.Dialogue.CycleUp.performed += OnCycleUp;
        inputActions.Dialogue.CycleDown.performed += OnCycleDown;
    }

    private void OnDisable()
    {
        inputActions.Dialogue.CycleUp.performed -= OnCycleUp;
        inputActions.Dialogue.CycleDown.performed -= OnCycleDown;
    }

    // CycleUp (up arrow) moves selection DOWN (reversed)
    private void OnCycleUp(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!dialogueIsPlaying || currentStory == null) return;
        if (currentStory.currentChoices.Count == 0) return;
        ChangeChoiceSelection(1);
    }

    // CycleDown (down arrow) moves selection UP (reversed)
    private void OnCycleDown(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        if (!dialogueIsPlaying || currentStory == null) return;
        if (currentStory.currentChoices.Count == 0) return;
        ChangeChoiceSelection(-1);
    }

    public static DialogueManager GetInstance()
    {
        return instance;
    }

    private void Start()
    {
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);

        LayoutAnimator = dialoguePanel.GetComponent<Animator>();
        
        choicesText = new TextMeshProUGUI[choices.Length];
        int index = 0;
        foreach (GameObject choice in choices)
        {
            choicesText[index] = choice.GetComponentInChildren<TextMeshProUGUI>();
            index++;
        }
    }

    private void Update()
    {
        if (!dialogueIsPlaying) return;

        if (submitConsumed)
        {
            submitConsumed = false;
            return;
        }

        if (canContinueToNextLine 
            && currentStory.currentChoices.Count == 0
            && inputActions.Dialogue.Submit.WasPressedThisFrame())
        {
            ContinueStory();
        }
    }

    public void EnterDialogueMode(TextAsset inkJson)
    {
        currentStory = new Story(inkJson.text);
        dialogueIsPlaying = true;
        dialoguePanel.SetActive(true);
        
        displayNameText.text = "???";
        portraitAnimator.Play("Default");
        LayoutAnimator.Play("Right");
        
        ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.2f);
        
        dialogueIsPlaying = false;
        dialoguePanel.SetActive(false);
        dialogueText.text = "";
    }

    private void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if (displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }
            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
            canContinueToNextLine = true;
            // handle tags
            HandleTags(currentStory.currentTags);
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        // display empty line
        dialogueText.text = "";
        
        continueIcon.SetActive(false);
        HideChoices();
        
        canContinueToNextLine = false;
        
        foreach (char letter in line.ToCharArray())
        {
            
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        
        continueIcon.SetActive(true);
        
        DisplayChoices();
        
        canContinueToNextLine = true;
    }

    private void HideChoices()
    {
        foreach (GameObject choiceButton in choices)
        {
            choiceButton.SetActive(false);
        }
    }

    private void HandleTags(List<string> currentTags)
    {
        // loop through each tag and handle it accordingly
        foreach (string tag in currentTags)
        {
            // parse the tag
            string[] splitTags = tag.Split(':');
            if (splitTags.Length != 2)
            {
                Debug.LogError("Tag could not be parsed: " + tag);
            }
            string tagKey = splitTags[0].Trim();
            string tagValue = splitTags[1].Trim();
            
            // handle the tag
            switch (tagKey)
            {
                case SPEAKER_TAG:
                    displayNameText.text = tagValue;
                    break;
                case PORTRAIT_TAG:
                    portraitAnimator.Play(tagValue);
                    break;
                case LAYOUT_TAG:
                    LayoutAnimator.Play(tagValue);
                    break;
                default:
                    Debug.LogError("Tag came in but is not currently being handled: " + tag);
                    break;
            }
        }
    }

    private void DisplayChoices()
    {
        List<Choice> currentChoices = currentStory.currentChoices;
        
        if (currentChoices.Count > choices.Length)
        {
            Debug.LogError("More choices were given than the UI can support. Number of choices given: " 
                           + currentChoices.Count);
        }
        
        int index = 0;
        foreach (Choice choice in currentChoices)
        {
            choices[index].gameObject.SetActive(true);
            choicesText[index].text = choice.text;
            index++;
        }
        for (int i = index; i < choices.Length; i++)
        {
            choices[i].gameObject.SetActive(false);
        }

        if (currentChoices.Count > 0)
            StartCoroutine(SelectFirstChoice());
    }

    private void ChangeChoiceSelection(int direction)
    {
        if (currentStory == null || currentStory.currentChoices.Count == 0) return;

        List<Choice> currentChoices = new List<Choice>(currentStory.currentChoices);
        currentChoiceIndex = (currentChoiceIndex + direction + currentChoices.Count) % currentChoices.Count;
        EventSystem.current.SetSelectedGameObject(choices[currentChoiceIndex].gameObject);
    }

    private IEnumerator SelectFirstChoice()
    {
        currentChoiceIndex = 0;
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choices[0].gameObject);
    }

    // Called by choice buttons via OnClick in the Inspector
    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            submitConsumed = true;
            ContinueStory();
        }
    }
    
    private void OnDestroy()
    {
        inputActions.Disable();
    }
}