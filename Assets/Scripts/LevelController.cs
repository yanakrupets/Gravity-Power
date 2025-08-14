using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private static int _count;
    
    public static int LevelsCount => _count == 0 ? CalculateTotalLevels() : _count;

    public static void LoadLevel(int level)
    {
        SceneManager.LoadScene(Consts.LevelScenePrefix + level);
    }

    public static void LoadStart()
    {
        SceneManager.LoadScene(Consts.StartScene);
    }

    public static void LoadLast()
    {
        SceneManager.LoadScene(Consts.LastScene);
    }

    public static bool TryGetNextLevel(out int nextLevel)
    {
        nextLevel = -1;
        var currentSceneName = SceneManager.GetActiveScene().name;
    
        if (!currentSceneName.StartsWith(Consts.LevelScenePrefix))
        {
            return false;
        }
    
        var levelNumberStr = currentSceneName.Substring(Consts.LevelScenePrefix.Length);
        if (!int.TryParse(levelNumberStr, out var currentLevel))
        {
            return false;
        }
    
        nextLevel = currentLevel + 1;
    
        var nextSceneName = Consts.LevelScenePrefix + nextLevel;
        if (Application.CanStreamedLevelBeLoaded(nextSceneName))
        {
            return true;
        }
        
        nextLevel = -1;
        return false;
    }

    public static void ReloadCurrentLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public static int GetLastAvailableLevel()
    {
        var lastAvailable = SaveController.GetLastLevel();
        var nextSceneName = Consts.LevelScenePrefix + (lastAvailable + 1);
        
        if (Application.CanStreamedLevelBeLoaded(nextSceneName))
        {
            return lastAvailable + 1;
        }
        
        return lastAvailable;
    }
    
    private static int CalculateTotalLevels()
    {
        var count = 0;
        while (Application.CanStreamedLevelBeLoaded($"{Consts.LevelScenePrefix}{count}"))
        {
            count++;
        }
        return count;
    }
}
