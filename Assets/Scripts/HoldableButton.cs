using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public class HoldableButton : MonoBehaviour
{
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;
    
    [Space]
    [SerializeField] private UnityEvent onInteract;
    [SerializeField] private UnityEvent onStopInteract;
    
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }
    
    private void Interact(bool isOn)
    {
        SoundController.Play(SoundType.PlayableButton);
        _spriteRenderer.sprite = isOn ? activeSprite : inactiveSprite;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(Consts.PlayerTag) || other.CompareTag(Consts.CrateTag))
        {
            Interact(false);
            onInteract?.Invoke();
        }
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(Consts.PlayerTag) || other.CompareTag(Consts.CrateTag))
        {
            Interact(true);
            onStopInteract?.Invoke();
        }
    }
}