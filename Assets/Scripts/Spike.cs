using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private GameObject spike;

    public bool IsActive { get; private set; }

    public void SetState(bool isActive)
    {
        IsActive = isActive;
        spike.SetActive(IsActive);
    }
}
