using UnityEngine;

public class DeliveryCounterVisual : SelectedCounterVisual
{
    [SerializeField] private ParticleSystem correctOrderParticles;
    [SerializeField] private ParticleSystem wrongOrderParticls;

    void Start()
    {
        (GetBaseCounter() as DeliveryCounter).OnCorrectDelivery += DeliveryCounterVisual_OnCorrectDelivery;
        (GetBaseCounter() as DeliveryCounter).OnWrongDelivery += DeliveryCounterVisual_OnWrongDelivery; ;
    }

    private void DeliveryCounterVisual_OnCorrectDelivery()
    {
        correctOrderParticles.Play();
    }

    private void DeliveryCounterVisual_OnWrongDelivery()
    {
        wrongOrderParticls.Play();
    }
}
