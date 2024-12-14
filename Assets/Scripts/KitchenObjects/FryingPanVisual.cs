using UnityEngine;

public class FryingPanVisual : MonoBehaviour
{
    [SerializeField] private FryingPan fryingPanParent;
    [SerializeField] private ParticleSystem sizzlingOil;
    [SerializeField] private ParticleSystem fire;

    void Start()
    {
        fryingPanParent.OnFrying += FryingPanParent_OnFrying;
        fryingPanParent.OnBurningStarted += FryingPanParent_OnBurning; ;
    }

    private void FryingPanParent_OnBurning(bool isBurning)
    {
        if (isBurning)
        {
            fire.Play();
        }
        else
        {
            fire.Stop();
        }
    }

    private void FryingPanParent_OnFrying(bool isFrying)
    {
        if (isFrying) 
        {
            sizzlingOil.Play();            
        }
        else
        {
            sizzlingOil.Stop();
        }
    }

    void Update()
    {
        
    }
}
