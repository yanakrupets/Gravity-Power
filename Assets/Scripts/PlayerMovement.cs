using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.2f, 0.2f);
    [SerializeField] private LayerMask groundLayer;
    
    [Space]
    [SerializeField] private GravityController gravityController;
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private Physic2DElement playerPhysic2D;
    [SerializeField] private InputActionAsset inputActionAsset;
    
    private float _horizontal;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private bool _isFacingRight;
    
    private void Awake()
    {
        _moveAction = inputActionAsset.FindAction("Move");
        _jumpAction = inputActionAsset.FindAction("Jump");
        
        _isFacingRight = true;
    }
    
    private void OnEnable()
    {
        _moveAction.performed += Move;
        _moveAction.canceled += StopMove;
        _jumpAction.performed += Jump;
        
        playerPhysic2D.OnGravityChanged += UpdateFacingDirection;
        
        _moveAction.Enable();
        _jumpAction.Enable();
    }

    private void OnDisable()
    {
        _moveAction.performed -= Move;
        _moveAction.canceled -= StopMove;
        _jumpAction.performed -= Jump;
        
        _moveAction.Disable();
        _jumpAction.Disable();
        
        if (playerPhysic2D != null)
            playerPhysic2D.OnGravityChanged -= UpdateFacingDirection;
    }

    private void Update()
    {
        playerPhysic2D.Rb.velocity = new Vector2(_horizontal * speed, playerPhysic2D.Rb.velocity.y);
        
        var velocityForAnimation = gravityController.IsPositive 
            ? playerPhysic2D.Rb.velocity.y 
            : -playerPhysic2D.Rb.velocity.y;
        
        playerAnimation.UpdateMoveState(_horizontal, velocityForAnimation, IsGrounded());
    }
    
    public bool IsGrounded()
    {
        return Physics2D.OverlapBox(groundCheck.position, groundCheckSize, 0f, groundLayer);
    }

    private void Move(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
        UpdateFacingDirection();
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

    private void UpdateFacingDirection()
    {
        if (_horizontal == 0) return;
    
        var shouldFaceRight = _horizontal > 0;
    
        if (!gravityController.IsPositive)
        {
            shouldFaceRight = !shouldFaceRight;
        }
    
        if (_isFacingRight != shouldFaceRight)
        {
            _isFacingRight = shouldFaceRight;
            playerAnimation.Flip(_isFacingRight);
        }
    }
    
    private void OnDrawGizmosSelected() 
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheck.position, groundCheckSize);
    }
}
