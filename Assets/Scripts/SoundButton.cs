using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class SoundButton : MonoBehaviour
{
    [SerializeField] private Sprite soundOnSprite;
    [SerializeField] private Sprite soundOffSprite;
    
    [SerializeField] private Image soundImage;
    
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
    }

    private void Start()
    {
        soundImage.sprite = SoundController.IsActive ? soundOnSprite : soundOffSprite;
    }

    private void OnEnable()
    {
        _button.onClick.AddListener(Activate);
    }

    private void OnDisable()
    {
        _button.onClick.RemoveListener(Activate);
    }

    private void Activate()
    {
        SoundController.Activate();
        soundImage.sprite = SoundController.IsActive ? soundOnSprite : soundOffSprite;
    }
}
