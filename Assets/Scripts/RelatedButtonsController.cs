using UnityEngine;
using UnityEngine.Events;

public class RelatedButtonsController : MonoBehaviour
{
    [Header("Button References")]
    [SerializeField] private RelatedButton buttonA;
    [SerializeField] private RelatedButton buttonB;
    
    [Header("Events")]
    [SerializeField] private UnityEvent onButtonAInteract;
    [SerializeField] private UnityEvent onButtonBInteract;
    
    private void Start()
    {
        buttonA.SetController(this);
        buttonB.SetController(this);
        
        SetInitialState();
    }

    private void SetInitialState()
    {
        buttonA.SetState(true);
        buttonB.SetState(false);
    }

    public void OnButtonPressed(RelatedButton pressedButton)
    {
        SoundController.Play(SoundType.PlayableButton);
        
        if (pressedButton == buttonA)
        {
            buttonA.SetState(false);
            buttonB.SetState(true);
            onButtonAInteract.Invoke();
        }
        else if (pressedButton == buttonB)
        {
            buttonA.SetState(true);
            buttonB.SetState(false);
            onButtonBInteract.Invoke();
        }
    }
}
