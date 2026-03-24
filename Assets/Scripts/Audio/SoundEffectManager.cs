using UnityEngine;
using UnityEngine.UI;

public class SoundEffectManager : MonoBehaviour
{
    private static SoundEffectManager _instance;
    private AudioSource audioSource;
    [SerializeField] private Slider sfxSlider;
    private float masterVolume = 1f;
    
    private string[] characterFolders = { "Askeladden", "Witch", "Huldra", "Benjamin", "Bugh", "Markus", "Niklas" };
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            audioSource = GetComponent<AudioSource>();
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
                audioSource.PlayOneShot(clip, masterVolume);
                return;
            }
        }
        Debug.LogWarning("Dialogue clip not found: " + clipName);
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