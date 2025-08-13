using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private PlayerAnimation playerAnimation;
    [SerializeField] private Collider2D mainCollider2D;
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Consts.SpikesTag))
        {
            HandleDeath();
        }
    }
    
    private void HandleDeath()
    {
        playerAnimation.PlayDeathAnimation();
        mainCollider2D.isTrigger = true;
    }
}