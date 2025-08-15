using UnityEngine;
using UnityEngine.InputSystem;

public class GravityController : MonoBehaviour
{
    [Header("Gravity Settings")]
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float maxFallSpeed = 15f;
    [SerializeField] private float fallSpeedMultiplier = 1.2f;
    
    [Space]
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private Physic2DElement[] physicElements;

    private InputAction _changeGravityAction;
    
    public bool IsPositive { get; private set; }

    private void Awake()
    {
        _changeGravityAction = inputActionAsset.FindAction("ChangeGravity");
        
        IsPositive = true;
    }

    private void OnEnable()
    {
        _changeGravityAction.performed += ChangeGravity;
        
        _changeGravityAction.Enable();
    }

    private void OnDisable()
    {
        _changeGravityAction.performed -= ChangeGravity;
        
        _changeGravityAction.Disable();
    }

    private void Update()
    {
        foreach (var physicElement in physicElements)
        {
            Gravity(physicElement.Rb);
            
            var targetRotation = IsPositive ? 0f : 180f;
            physicElement.transform.rotation = Quaternion.Lerp(physicElement.transform.rotation, 
                Quaternion.Euler(0, 0, targetRotation), Time.deltaTime * 10f);
        }
    }

    private void ChangeGravity(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (!playerMovement.IsGrounded()) return;
        
        SoundController.Play(SoundType.GravityChange);
        
        gravity = -gravity;
        IsPositive = gravity > 0;
        
        foreach (var element in physicElements)
        {
            element.GravityChanged();
        }
    }

    private void Gravity(Rigidbody2D rb)
    {
        var isMovingAgainstGravity = 
            (IsPositive && rb.velocity.y > 0) || (!IsPositive && rb.velocity.y < 0);

        if (!isMovingAgainstGravity)
        {
            rb.gravityScale = gravity * fallSpeedMultiplier;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -maxFallSpeed, maxFallSpeed));
        }
        else
        {
            rb.gravityScale = gravity;
        }
    }
}
