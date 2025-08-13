using UnityEngine;
using UnityEngine.UI;

public class ControlsCanvas : CanvasBase
{
    [SerializeField] private Button menuButton;

    private void Awake()
    {
        CanvasType = CanvasType.Controls;
    }

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
        Manager.OpenCanvas(CanvasType.Menu);
    }
}