using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int YVelocity = Animator.StringToHash("YVelocity");
    private static readonly int Grounded = Animator.StringToHash("IsGrounded");
    private static readonly int Death = Animator.StringToHash("Death");

    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 5f;
    
    [SerializeField] private Transform groundCheck;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.2f, 0.2f);
    [SerializeField] private LayerMask groundLayer;
    
    [SerializeField] private GravityController gravityController;
    [SerializeField] private InputActionAsset inputActionAsset;
    [SerializeField] private Physic2DElement playerPhysic2D;
    [SerializeField] private Collider2D mainCollider2D;
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;
    
    private float _horizontal;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private bool _isFacingRight;
    private bool _isMove;
    
    private void Awake()
    {
        _moveAction = inputActionAsset.FindAction("Move");
        _jumpAction = inputActionAsset.FindAction("Jump");
        
        _moveAction.performed += Move;
        _moveAction.canceled += StopMove;
        _jumpAction.performed += Jump;

        _isFacingRight = true;
    }
    
    private void OnEnable()
    {
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
    }

    private void Update()
    {
        playerPhysic2D.Rb.velocity = new Vector2(_horizontal * speed, playerPhysic2D.Rb.velocity.y);
        
        var velocityForAnimation = gravityController.IsPositive 
            ? playerPhysic2D.Rb.velocity.y 
            : -playerPhysic2D.Rb.velocity.y;
        
        animator.SetFloat(Speed, Mathf.Abs(_horizontal));
        animator.SetFloat(YVelocity, velocityForAnimation);
        animator.SetBool(Grounded, IsGrounded());
    }

    private void Move(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
        Flip();
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

    private void Flip()
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
            spriteRenderer.flipX = !_isFacingRight;
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
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Consts.SpikesTag))
        {
            animator.SetTrigger(Death);
            mainCollider2D.isTrigger = true;
        }
    }
}
