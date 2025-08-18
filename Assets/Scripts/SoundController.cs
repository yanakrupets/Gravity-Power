using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class SoundController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Sound[] sounds;

    private static SoundController Instance { get; set; }

    public static bool IsActive { get; private set; } = true;

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
        if (Instance is null)
        {
            Debug.LogWarning("Sound Controller is inactive");
            return;
        }
        
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

    public static void Activate()
    {
        IsActive = !IsActive;
        Instance.audioMixer.SetFloat(Consts.Volume, IsActive ? 0f : -80f);
    }
}
