using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject helpInputkey;
    [SerializeField] private InputActionAsset inputActionAsset;
    
    private const string PlayerTag = "Player";
    private const string KeyTag = "Key";

    private InputAction _completeLevelAction;
    private bool _playerInRange;
    private bool _keyInRange;
    
    private void Awake()
    {
        _completeLevelAction = inputActionAsset.FindAction("CompleteLevel");
        _completeLevelAction.performed += CompleteLevel;

        _completeLevelAction.Disable();
    }
    
    private void OnDisable() => _completeLevelAction.Disable();
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PlayerTag))
            _playerInRange = true;
        
        if (other.CompareTag(KeyTag))
            _keyInRange = true;

        SetEnabledHelpInputKey(_playerInRange && _keyInRange);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(PlayerTag))
            _playerInRange = false;
        
        if (other.CompareTag(KeyTag))
            _keyInRange = false;
        
        SetEnabledHelpInputKey(_playerInRange && _keyInRange);
    }

    private void SetEnabledHelpInputKey(bool isOn)
    {
        helpInputkey.SetActive(isOn);
        SetEnableInputAction(isOn);
    }

    private void SetEnableInputAction(bool isOn)
    {
        if (isOn)
        {
            _completeLevelAction.Enable();
        }
        else
        {
            _completeLevelAction.Disable();
        }
    }

    private void CompleteLevel(InputAction.CallbackContext context)
    {
        Debug.Log("Level completed");
    }
}
