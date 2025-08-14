using UnityEngine;

public class SaveController : MonoBehaviour
{
    public static void SaveLastLevel(int level)
    {
        var savedLevel = GetLastLevel();
        if (level > savedLevel)
        {
            PlayerPrefs.SetInt(Consts.LevelKey, level);
        }
    }

    public static void SaveLevelCompleteTime(int currentLevel, float completeTime)
    {
        var lastCompleteLevelTimeString = GetLevelCompleteTime(currentLevel);
        var lastCompleteLevelTime = TimeFormatter.ParseTimeToSeconds(lastCompleteLevelTimeString);

        if (completeTime >= lastCompleteLevelTime && lastCompleteLevelTime != 0) return;
        
        var time = TimeFormatter.GetFormatTime(completeTime);
        PlayerPrefs.SetString(Consts.TimeKey + currentLevel, time);
    }

    public static void SetEndGame(bool isGameOver)
    {
        PlayerPrefs.SetInt(Consts.EndGameKey, isGameOver ? 1 : 0);
    }

    public static int GetLastLevel()
    {
        return PlayerPrefs.GetInt(Consts.LevelKey);
    }

    public static string GetLevelCompleteTime(int level)
    {
        return PlayerPrefs.GetString(Consts.TimeKey + level, "0 min 0 sec");
    }

    public static bool IsGameEnded()
    {
        return PlayerPrefs.GetInt(Consts.EndGameKey) == 1;
    }
}
