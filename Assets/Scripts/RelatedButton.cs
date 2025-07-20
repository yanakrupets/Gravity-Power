using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RelatedButton : MonoBehaviour
{
    [SerializeField] private Sprite activeSprite;
    [SerializeField] private Sprite inactiveSprite;
    
    private const string PlayerTag = "Player";
    private const string CrateTag = "Crate";
    
    private RelatedButtonsController _controller;
    private SpriteRenderer _spriteRenderer;
    private bool _isActive;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetController(RelatedButtonsController controller)
    {
        _controller = controller;
    }

    public void SetState(bool isActive)
    {
        _isActive = isActive;
        _spriteRenderer.sprite = isActive ? activeSprite : inactiveSprite;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isActive && (other.CompareTag(PlayerTag) || other.CompareTag(CrateTag)))
        {
            _controller.OnButtonPressed(this);
        }
    }
}