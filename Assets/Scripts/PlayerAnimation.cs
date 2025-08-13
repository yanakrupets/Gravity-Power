using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int YVelocity = Animator.StringToHash("YVelocity");
    private static readonly int Grounded = Animator.StringToHash("IsGrounded");
    private static readonly int Death = Animator.StringToHash("Death");
    
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator animator;

    public void UpdateMoveState(float horizontal, float velocity, bool isGrounded)
    {
        animator.SetFloat(Speed, Mathf.Abs(horizontal));
        animator.SetFloat(YVelocity, velocity);
        animator.SetBool(Grounded, isGrounded);
    }

    public void PlayDeathAnimation()
    {
        animator.SetTrigger(Death);
    }
    
    public void Flip(bool isFlipped)
    {
        spriteRenderer.flipX = !isFlipped;
    }
}
