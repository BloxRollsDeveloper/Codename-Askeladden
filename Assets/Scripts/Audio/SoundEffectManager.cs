using UnityEngine;
using UnityEngine.UI;

public class SoundEffectManager : MonoBehaviour
{
    private static SoundEffectManager _instance;
    private AudioSource audioSource;
    private SoundEffectLibrary soundEffectLibrary;
    [SerializeField] private Slider sfxSlider;
    private float masterVolume = 1f;
    
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            audioSource = GetComponent<AudioSource>();
            soundEffectLibrary = GetComponent<SoundEffectLibrary>();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static void Play(string soundName)
    {
        if (_instance == null)
        {
            return;
        }

        float groupVolume;
        AudioClip clip = _instance.soundEffectLibrary.GetRandomClip(soundName, out groupVolume);

        if (clip != null)
        {
            float finalVolume = _instance.masterVolume * groupVolume;
            _instance.audioSource.PlayOneShot(clip, finalVolume);
        }
    }

    public void PlaySound(string soundName)
    {
        Play(soundName);
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