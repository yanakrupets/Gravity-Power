using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayableButton : MonoBehaviour
{
    [SerializeField] private Sprite inactiveSprite;
    
    [Space]
    [SerializeField] private UnityEvent onInteract;
    
    private SpriteRenderer _spriteRenderer;
    private bool _isActive;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_isActive && (other.CompareTag(Consts.PlayerTag) || other.CompareTag(Consts.CrateTag)))
        {
            _isActive = true;
            _spriteRenderer.sprite = inactiveSprite;
            onInteract?.Invoke();
        }
    }
}
