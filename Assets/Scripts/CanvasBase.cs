using UnityEngine;

public abstract class CanvasBase : MonoBehaviour
{
    [SerializeField] private CanvasType type;
    
    protected CanvasType CanvasType;
    protected CanvasManager Manager;

    public CanvasType Type => type;

    public void SetManager(CanvasManager manager)
    {
        Manager = manager;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}