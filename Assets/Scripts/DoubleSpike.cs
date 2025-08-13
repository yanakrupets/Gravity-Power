using UnityEngine;

public class DoubleSpike : MonoBehaviour
{
    [SerializeField] private Spike spikeA;
    [SerializeField] private Spike spikeB;

    private void Start()
    {
        SetInitialState();
    }
    
    public void Swap()
    {
        spikeA.SetState(!spikeA.IsActive);
        spikeB.SetState(!spikeB.IsActive);
    }

    private void SetInitialState()
    {
        spikeA.SetState(true);
        spikeB.SetState(false);
    }
}
