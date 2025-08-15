using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private GameObject spike;

    public bool IsActive { get; private set; }

    public void SetActive(bool isActive)
    {
        SetState(isActive);
        SoundController.Play(isActive ? SoundType.SpikesOpened : SoundType.SpikesClosed);
    }

    public void SetState(bool isActive)
    {
        IsActive = isActive;
        spike.SetActive(IsActive);
    }
}
