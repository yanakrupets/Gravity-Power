using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : CanvasBase
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button controlsButton;
    [SerializeField] private Button levelsButton;
    
    // test
    [SerializeField] private Button resetButton;

    private void OnEnable()
    {
        playButton.onClick.AddListener(StartGame);
        controlsButton.onClick.AddListener(OpenControls);
        levelsButton.onClick.AddListener(OpenLevels);
        
        // test
        resetButton.onClick.AddListener(Reset);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(StartGame);
        controlsButton.onClick.RemoveListener(OpenControls);
        levelsButton.onClick.RemoveListener(OpenLevels);
        
        // test
        resetButton.onClick.RemoveListener(Reset);
    }

    private void StartGame()
    {
        if (SaveController.IsGameEnded())
        {
            var lastLevel = LevelController.GetLastAvailableLevel();

            if (SaveController.GetLastLevel() != lastLevel)
            {
                SaveController.SaveLastLevel(lastLevel);
                SaveController.SetEndGame(false);
            }
            
            LevelController.LoadLevel(lastLevel);
        }
        else
        {
            LevelController.LoadLevel(SaveController.GetLastLevel());
        }
    }
    
    // test
    private void Reset()
    {
        PlayerPrefs.DeleteAll();
    }

    private void OpenControls()
    {
        Manager.OpenCanvas(CanvasType.Controls);
    }

    private void OpenLevels()
    {
        Manager.OpenCanvas(CanvasType.Levels);
    }
}
