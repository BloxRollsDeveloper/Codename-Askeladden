using UnityEngine;
using UnityEngine.UI;

public class SoundEffectManager : MonoBehaviour
{
    private static SoundEffectManager _instance;
    private AudioSource audioSource;
    private AudioSource dialogueAudioSource;
    [SerializeField] private Slider sfxSlider;
    private float masterVolume = 1f;
    
    private string[] characterFolders = { "Askeladden", "Witch", "Huldra", "Benjamin", "Bugh", "Markus", "Niklas" };
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            audioSource = GetComponent<AudioSource>();
            
            dialogueAudioSource = gameObject.AddComponent<AudioSource>(); // Seperate audio source for dialogue
            
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayDialogueClip(string clipName)
    {
        foreach (string character in characterFolders)
        {
            AudioClip clip = Resources.Load<AudioClip>($"Dialogue/{character}/{clipName}");
            if (clip != null)
            {
                dialogueAudioSource.Stop();
                dialogueAudioSource.clip = clip;
                dialogueAudioSource.volume = masterVolume;
                dialogueAudioSource.Play();
                return;
            }
        }
    }

    private void Start()
    {
        if (sfxSlider != null)
        {
            sfxSlider.onValueChanged.AddListener(delegate { OnValueChange(); });
            masterVolume = sfxSlider.value;
        }
    }

    public void SetVolume(float volume)
    {
        masterVolume = volume;
    }

    public void OnValueChange()
    {
        SetVolume(sfxSlider.value);
    }
}