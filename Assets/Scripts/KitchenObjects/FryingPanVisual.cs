using UnityEngine;

public class FryingPanVisual : MonoBehaviour
{
    [SerializeField] private FryingPan fryingPanParent;
    [SerializeField] private ParticleSystem sizzlingOil;
    [SerializeField] private ParticleSystem fire;

    void Start()
    {
        fryingPanParent.OnMeatStateChanged += FryingPanParent_OnMeatStateChanged;
    }

    private void FryingPanParent_OnMeatStateChanged(object sender, FryingPan.OnMeatStateChangedEventArgs e)
    {
        if (sender is FryingPan fryingPan)
        {
            switch(fryingPan.GetCurrentState()) {
                case FryingPan.State.Frying:
                    sizzlingOil.Play(); 
                    fire.Stop();
                    break;
                case FryingPan.State.InFlames:
                    fire.Play();
                    sizzlingOil.Stop();
                    break;
                default:
                    sizzlingOil.Stop();
                    fire.Stop();
                    break;
            }
        }
    }
}
