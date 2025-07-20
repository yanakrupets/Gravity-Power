using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class HoldableButton : MonoBehaviour
{
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;
    
    [SerializeField] private UnityEvent onInteract;
    [SerializeField] private UnityEvent onStopInteract;
    
    private const string PlayerTag = "Player";
    private const string CrateTag = "Crate";
    
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Interact(bool isOn)
    {
        _spriteRenderer.sprite = isOn ? activeSprite : inactiveSprite;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PlayerTag) || other.CompareTag(CrateTag))
        {
            Interact(false);
            onInteract?.Invoke();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(PlayerTag) || other.CompareTag(CrateTag))
        {
            Interact(true);
            onStopInteract?.Invoke();
        }
    }
}