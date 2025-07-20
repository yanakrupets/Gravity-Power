using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    [SerializeField] private GameObject spike;
    
    private bool _isActive;

    public bool IsActive => _isActive;
    
    public void SetState(bool isActive)
    {
        _isActive = isActive;
        spike.SetActive(_isActive);
    }
}
