using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.2f, 0.2f);
    [SerializeField] private LayerMask groundLayer;
    
    [SerializeField] private GravityController gravityController;
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private Physic2DElement playerPhysic2D;
    
    private float _horizontal;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    
    private void Awake()
    {
        _moveAction = inputActionAsset.FindAction("Move");
        _jumpAction = inputActionAsset.FindAction("Jump");
        
        _moveAction.performed += Move;
        _moveAction.canceled += StopMove;
        _jumpAction.performed += Jump;
    }
    
    private void OnEnable()
    {
        _moveAction.Enable();
        _jumpAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.Disable();
        _jumpAction.Disable();
    }

    private void Update()
    {
        playerPhysic2D.Rb.velocity = new Vector2(_horizontal * speed, playerPhysic2D.Rb.velocity.y);
    }

    private void Move(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
    }

    private void StopMove(InputAction.CallbackContext context)
    {
        _horizontal = 0f;
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (!IsGrounded())
        {
            return;
        }
        
        if (context.performed)
        {
            float direction = gravityController.IsPositive ? 1 : -1;
            playerPhysic2D.Rb.velocity = new Vector2(playerPhysic2D.Rb.velocity.x, jumpForce * direction);
        }
    }
    
    private bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer);
    }
    
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }
}
