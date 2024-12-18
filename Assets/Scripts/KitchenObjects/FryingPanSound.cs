using UnityEngine;

public class FryingPanSound : MonoBehaviour
{
    [SerializeField] private FryingPan parentFryingPan;
    [SerializeField] private AudioSource fryingMeat;
    [SerializeField] private AudioSource warnBurningMeat;
    [SerializeField] private AudioSource meatInFlames;

    private void Awake()
    {
        parentFryingPan.OnMeatStateChanged += FryingPan_OnMeatStateChanged;
    }

    private void FryingPan_OnMeatStateChanged(object sender, FryingPan.OnMeatStateChangedEventArgs e)
    {
        if (sender is FryingPan fryingPan)
        {
            switch(fryingPan.GetCurrentState())
            {
                case FryingPan.State.Frying:
                    fryingMeat.Play();
                    break;
                case FryingPan.State.Burning:
                    fryingMeat.Stop();
                    warnBurningMeat.Play();
                    break;
                case FryingPan.State.InFlames:
                    warnBurningMeat.Stop();
                    meatInFlames.Play();
                    break;
                default:
                    fryingMeat.Stop();
                    warnBurningMeat.Stop();
                    meatInFlames.Stop();
                    break;
            }
        }
    }
}
