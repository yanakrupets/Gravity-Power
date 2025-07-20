using UnityEngine;
using UnityEngine.InputSystem;

public class GravityController : MonoBehaviour
{
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float maxFallSpeed = 15f;
    [SerializeField] private float fallSpeedMultiplier = 1.2f;
    
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private Physic2DElement[] physicElements;
    
    private bool _isPositive;
    private InputAction _changeGravityAction;
    
    public bool IsPositive => _isPositive;

    private void Awake()
    {
        _changeGravityAction = inputActionAsset.FindAction("ChangeGravity");
        _changeGravityAction.performed += ChangeGravity;
    }

    private void OnEnable()
    {
        _changeGravityAction.Enable();
    }

    private void OnDisable()
    {
        _changeGravityAction.performed -= ChangeGravity;
        
        _changeGravityAction.Disable();
    }

    private void Start()
    {
        _isPositive = true;
    }

    private void Update()
    {
        foreach (var physicElement in physicElements)
        {
            Gravity(physicElement.Rb);
            
            var targetRotation = _isPositive ? 0f : 180f;
            physicElement.transform.rotation = Quaternion.Lerp(physicElement.transform.rotation, 
                Quaternion.Euler(0, 0, targetRotation), Time.deltaTime * 10f);
        }
    }

    private void ChangeGravity(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        gravity = -gravity;
        _isPositive = gravity > 0;
    }

    private void Gravity(Rigidbody2D rb)
    {
        var isMovingAgainstGravity = 
            (_isPositive && rb.velocity.y > 0) || (!_isPositive && rb.velocity.y < 0);

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
