using System.Collections.Generic;
using UnityEngine;

public class SoundEffectLibrary : MonoBehaviour
{
    [SerializeField] private SoundEffectGroup[] soundEffectGroups;
    private Dictionary<string, SoundEffectGroup> soundDictionary;


    private void Awake()
    {
        InitializeDictionary();
    }

    private void InitializeDictionary()
    {
        soundDictionary = new Dictionary<string, SoundEffectGroup>();

        foreach (SoundEffectGroup group in soundEffectGroups)
        {
            soundDictionary[group.name] = group;
        }
    }


    public AudioClip GetRandomClip(string name, out float volume)
    {
        volume = 1f;
        if (soundDictionary.ContainsKey(name))
        {
            SoundEffectGroup group = soundDictionary[name];
            if (group.audioClips.Count > 0)
            {
                volume = group.volume;
                return group.audioClips[UnityEngine.Random.Range(0, group.audioClips.Count)];
            }
        }

        return null;
    }

}

[System.Serializable]
public struct SoundEffectGroup
{
    public string name;
    public List<AudioClip> audioClips;
    [Range(0f, 1f)] public float volume;
}