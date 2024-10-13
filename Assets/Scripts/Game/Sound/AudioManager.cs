using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] soundClips;

    private Dictionary<string, AudioClip> soundDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            soundDictionary = new Dictionary<string, AudioClip>();
            foreach (var clip in soundClips)
            {
                soundDictionary.Add(clip.name, clip);
            }
        }
        else
        {
            Destroy(gameObject); // Enforce singleton pattern
        }
    }

    public void PlaySound(string soundName)
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            audioSource.clip = soundDictionary[soundName];
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Sound not found: " + soundName);
        }
    }
}