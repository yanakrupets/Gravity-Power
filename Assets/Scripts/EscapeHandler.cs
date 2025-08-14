using UnityEngine;
using UnityEngine.InputSystem;

public class EscapeHandler : MonoBehaviour
{
    [SerializeField] private InputActionAsset inputActionAsset;
    
    private InputAction _openMenuAction;

    private void Awake()
    {
        _openMenuAction = inputActionAsset.FindAction("MenuOpen");
    }

    private void OnEnable()
    {
        _openMenuAction.performed += LoadStart;
        
        _openMenuAction.Enable();
    }

    private void OnDisable()
    {
        _openMenuAction.performed -= LoadStart;
        
        _openMenuAction.Disable();
    }
    
    private void LoadStart(InputAction.CallbackContext context)
    {
        LevelController.LoadStart();
    }
}
