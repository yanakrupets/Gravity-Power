using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{
    [SerializeField] private Button levelButton;
    [SerializeField] private GameObject blockImage;

    private int _level;

    private void OnEnable()
    {
        levelButton.onClick.AddListener(LoadLevel);
    }

    private void OnDisable()
    {
        levelButton.onClick.RemoveListener(LoadLevel);
    }

    public void Initialize(int level, Sprite levelSprite, bool isOpen)
    {
        _level = level;
        levelButton.image.sprite = levelSprite;
        
        SetActive(isOpen);
    }

    private void SetActive(bool isActive)
    {
        levelButton.interactable = isActive;
        blockImage.SetActive(!isActive);
    }

    private void LoadLevel()
    {
        LevelController.LoadLevel(_level);
    }
}
