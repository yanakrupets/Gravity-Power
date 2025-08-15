using System.Linq;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Sound[] sounds;

    private static SoundController Instance { get; set; }
    
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
    }
    
    public static void Play(SoundType soundType)
    {
        var sound = Instance.sounds.FirstOrDefault(sound => sound.type == soundType);
        if (sound != null)
        {
            Instance.audioSource.PlayOneShot(sound.clip);
        }
        else
        {
            Debug.LogWarning($"Sound {soundType} not found!");
        }
    }
}
