using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private StoveCounter stoveParentCounter;
    [SerializeField] private ParticleSystem stoveFlame;

    void Start()
    {
        stoveParentCounter.OnStoveStateChanged += StoveParentCounter_OnStoveStateChanged;
    }

    private void StoveParentCounter_OnStoveStateChanged(bool isOn)
    {
        if (isOn)
        {
            stoveFlame.Play();
        }
        else
        {
            stoveFlame.Stop();
        }
    }
}
