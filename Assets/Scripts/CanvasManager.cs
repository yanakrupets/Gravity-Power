using System.Linq;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    [SerializeField] private CanvasBase[] canvases;
    
    private CanvasBase _currentCanvas;

    private void Awake()
    {
        foreach (var canvas in canvases)
        {
            canvas.SetManager(this);
        }
        
        _currentCanvas = canvases.Single(canvas => canvas.Type == CanvasType.Menu);
    }

    public void OpenCanvas(CanvasType type)
    {
        _currentCanvas.Hide();
        _currentCanvas = canvases.Single(canvas => canvas.Type == type);
        _currentCanvas.Show();
    }
}