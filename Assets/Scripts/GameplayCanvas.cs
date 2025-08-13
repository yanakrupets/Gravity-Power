using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvas : MonoBehaviour
{
    [SerializeField] private Button reloadButton;

    private void Awake()
    {
        reloadButton.onClick.AddListener(Reload);
    }

    private void OnDisable()
    {
        reloadButton.onClick.RemoveListener(Reload);
    }

    private void Reload()
    {
        LevelController.ReloadCurrentLevel();
    }
}
