using UnityEngine;

public class FryingPanSound : MonoBehaviour
{
    [SerializeField] private FryingPan parentFryingPan;
    [SerializeField] private AudioSource fryingMeat;
    [SerializeField] private AudioSource warnBurningMeat;
    [SerializeField] private AudioSource meatInFlames;

    private void Awake()
    {
        parentFryingPan.OnFrying += FryingPan_OnFrying;
        parentFryingPan.OnBurningStarted += FryingPan_OnBurningStarted;
    }

    private void FryingPan_OnFrying(bool isFrying)
    {
        if (isFrying)
        {
            fryingMeat.Play();
        }
        else
        {
            fryingMeat.Stop();
        }
    }

    private void FryingPan_OnBurningStarted(bool startedBurning)
    {
        
        if (startedBurning)
        {
            warnBurningMeat.Play();
        }
        else
        {
            warnBurningMeat.Stop();
        }
    }
}
