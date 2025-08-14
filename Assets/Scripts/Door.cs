using UnityEngine;
using UnityEngine.InputSystem;

public class Door : MonoBehaviour
{
    [SerializeField] private GameObject helpInputKey;
    [SerializeField] private InputActionAsset inputActionAsset;

    private InputAction _completeLevelAction;
    private bool _playerInRange;
    private bool _keyInRange;
    
    private void Awake()
    {
        _completeLevelAction = inputActionAsset.FindAction("CompleteLevel");
        _completeLevelAction.performed += CompleteLevel;

        _completeLevelAction.Disable();
    }

    private void OnDisable()
    {
        _completeLevelAction.performed -= CompleteLevel;
        
        _completeLevelAction.Disable();
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Consts.PlayerTag))
            _playerInRange = true;
        
        if (other.CompareTag(Consts.KeyTag))
            _keyInRange = true;

        SetEnabledHelpInputKey(_playerInRange && _keyInRange);
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(Consts.PlayerTag))
            _playerInRange = false;
        
        if (other.CompareTag(Consts.KeyTag))
            _keyInRange = false;
        
        SetEnabledHelpInputKey(_playerInRange && _keyInRange);
    }

    private void SetEnabledHelpInputKey(bool isOn)
    {
        helpInputKey.SetActive(isOn);
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
        SaveController.SaveLevelCompleteTime(LevelController.CurrentLevel, Time.timeSinceLevelLoad);
        
        if (LevelController.TryGetNextLevel(out var nextLevel))
        {
            SaveController.SaveLastLevel(nextLevel);
            LevelController.LoadLevel(nextLevel);
        }
        else
        {
            SaveController.SetEndGame(true);
            LevelController.LoadLast();
        }
    }
}
