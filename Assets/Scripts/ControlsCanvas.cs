using UnityEngine;
using UnityEngine.UI;

public class ControlsCanvas : CanvasBase
{
    [SerializeField] private Button menuButton;

    private void OnEnable()
    {
        menuButton.onClick.AddListener(OpenMenu);
    }

    private void OnDisable()
    {
        menuButton.onClick.RemoveListener(OpenMenu);
    }

    private void OpenMenu()
    {
        SoundController.Play(SoundType.UIButton);
        Manager.OpenCanvas(CanvasType.Menu);
    }
}