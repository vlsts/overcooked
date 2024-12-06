using System;
using UnityEngine;

public class SinkCounterVisual : MonoBehaviour
{
    [SerializeField] private SinkCounter sinkParentCounter;
    [SerializeField] private ParticleSystem waterflow;


    void Start()
    {
        sinkParentCounter.OnTapToggle += SinkParentCounter_OnTapToggle;
    }

    private void SinkParentCounter_OnTapToggle(bool isWashing)
    {
        if (isWashing)
        {
            waterflow.Play();
        }
        else
        {
            waterflow.Stop();
        }
    }
}
