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
        spikeA.SetActive(!spikeA.IsActive);
        spikeB.SetActive(!spikeB.IsActive);
    }

    private void SetInitialState()
    {
        spikeA.SetState(true);
        spikeB.SetState(false);
    }
}
