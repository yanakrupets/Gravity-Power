using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class LevelsCanvas : CanvasBase
{
    [SerializeField] private Button menuButton;
    [SerializeField] private LevelButton levelButtonPrefab;
    [SerializeField] private Transform levelButtonsParent;
    [SerializeField] private SpriteAtlas levelPreviewsAtlas;
    
    private readonly List<LevelButton> _levelButtons = new();

    private void Awake()
    {
        CanvasType = CanvasType.Levels;
        
        for (var i = 0; i < LevelController.LevelsCount; i++)
        {
            var levelButton = Instantiate(levelButtonPrefab, levelButtonsParent);
            _levelButtons.Add(levelButton);
        }
    }
    
    private void OnEnable()
    {
        menuButton.onClick.AddListener(OpenMenu);
        
        var lastOpenedLevel = SaveController.GetLastLevel();
        for (var i = 0; i < _levelButtons.Count; i++)
        {
            _levelButtons[i].Initialize(
                i, 
                GetLevelPreview(i), 
                SaveController.GetLevelCompleteTime(i), 
                i <= lastOpenedLevel);
        }
    }
    
    private void OnDisable()
    {
        menuButton.onClick.RemoveListener(OpenMenu);
    }
    
    private Sprite GetLevelPreview(int levelNumber)
    {
        return levelPreviewsAtlas.GetSprite($"level_{levelNumber}_preview");
    }

    private void OpenMenu()
    {
        Manager.OpenCanvas(CanvasType.Menu);
    }
}
