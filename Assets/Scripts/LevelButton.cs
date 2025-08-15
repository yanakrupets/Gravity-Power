using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button levelButton;
    [SerializeField] private GameObject blockImage;
    [SerializeField] private Text completeTimeText;
    [SerializeField] private GameObject completeTimeObject;

    private int _level;

    private void OnEnable()
    {
        levelButton.onClick.AddListener(LoadLevel);
    }

    private void OnDisable()
    {
        levelButton.onClick.RemoveListener(LoadLevel);
    }

    public void Initialize(int level, Sprite levelSprite, string time, bool isOpen)
    {
        _level = level;
        levelButton.image.sprite = levelSprite;
        completeTimeText.text = time;
        
        SetActive(isOpen);
    }

    private void SetActive(bool isActive)
    {
        levelButton.interactable = isActive;
        blockImage.SetActive(!isActive);
        completeTimeObject.SetActive(isActive);
    }

    private void LoadLevel()
    {
        SoundController.Play(SoundType.UIButton);
        LevelController.LoadLevel(_level);
    }
}
