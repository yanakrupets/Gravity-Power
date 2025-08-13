using UnityEngine;

public class SaveController : MonoBehaviour
{
    // add each level score ?
    // time to complete level

    public static void SaveLastLevel(int level)
    {
        var savedLevel = GetLastLevel();
        if (level > savedLevel)
        {
            PlayerPrefs.SetInt(Consts.LevelKey, level);
        }
    }

    public static void SetEndGame(bool isGameOver)
    {
        PlayerPrefs.SetInt(Consts.EndGameKey, isGameOver ? 1 : 0);
    }

    public static int GetLastLevel()
    {
        return PlayerPrefs.GetInt(Consts.LevelKey);
    }

    public static bool IsGameEnded()
    {
        return PlayerPrefs.GetInt(Consts.EndGameKey) == 1;
    }
}
