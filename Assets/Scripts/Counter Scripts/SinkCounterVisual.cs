using UnityEngine;

public class SinkCounterVisual : MonoBehaviour
{
    [SerializeField] private SinkCounter sinkParentCounter;
    [SerializeField] private ParticleSystem waterflow;

    void Start()
    {
        sinkParentCounter.OnWashingDish += SinkParentCounter_OnWashingDish;
    }

    private void SinkParentCounter_OnWashingDish()
    {
        waterflow.Play();
    }
}
