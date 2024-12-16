using System;
using UnityEngine;

public class SinkCounterVisual : MonoBehaviour
{
    [SerializeField] private SinkCounter sinkParentCounter;
    [SerializeField] private ParticleSystem waterflow;

    private Animator animator;
    private const string IS_WASHING = "IsWashing";

    void Start()
    {
        sinkParentCounter.OnTapToggle += SinkParentCounter_OnTapToggle;
        animator = sinkParentCounter.GetComponent<Animator>();
    }

    private void SinkParentCounter_OnTapToggle(bool isWashing)
    {
        if (isWashing)
        {
            waterflow.Play();
            animator.SetBool(IS_WASHING, true);
        }
        else
        {
            waterflow.Stop();
            animator.SetBool(IS_WASHING, false);
        }
    }
}
