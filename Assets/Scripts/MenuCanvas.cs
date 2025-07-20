using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuCanvas : MonoBehaviour
{
    [SerializeField] private Button playButton;

    private void Awake()
    {
        playButton.onClick.AddListener(StartGame);
    }

    private void StartGame()
    {
        SceneManager.LoadScene("Level 1");
    }
}
