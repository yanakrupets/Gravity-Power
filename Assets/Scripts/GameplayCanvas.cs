using UnityEngine;
using UnityEngine.UI;

public class GameplayCanvas : MonoBehaviour
{
    [SerializeField] private Button reloadButton;

    private void OnEnable()
    {
        reloadButton.onClick.AddListener(Reload);
    }

    private void OnDisable()
    {
        reloadButton.onClick.RemoveListener(Reload);
    }

    private void Reload()
    {
        SoundController.Play(SoundType.UIButton);
        LevelController.ReloadCurrentLevel();
    }
}
