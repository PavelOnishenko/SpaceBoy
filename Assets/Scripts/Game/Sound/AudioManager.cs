using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioSource audioSource;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
        [Range(0f, 1f)] public float volume = 1f; 
    }

    [SerializeField] private Sound[] sounds; 
    private Dictionary<string, Sound> soundDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        soundDictionary = new Dictionary<string, Sound>();
        foreach (Sound sound in sounds)
        {
            soundDictionary.Add(sound.name, sound);
        }
    }

    public void PlaySound(string soundName)
    {
        if (soundDictionary.ContainsKey(soundName))
        {
            Sound sound = soundDictionary[soundName];
            audioSource.clip = sound.clip;
            audioSource.volume = sound.volume;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning($"Sound {soundName} not found in AudioManager.");
        }
    }
}