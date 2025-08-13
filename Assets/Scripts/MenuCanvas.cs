using UnityEngine;
using UnityEngine.UI;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] private Button playButton;
    // levels button
    // settings button
    
    // test
    [SerializeField] private Button resetButton;

    private void Awake()
    {
        playButton.onClick.AddListener(StartGame);
        
        // test
        resetButton.onClick.AddListener(Reset);
    }

    private void OnDisable()
    {
        playButton.onClick.RemoveListener(StartGame);
        
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
}
