using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip deliveryAccepted;
    [SerializeField] private AudioClip deliveryRejected;

    public static SoundManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(Instance);
    }

    private void Start()
    {
        DeliveryCounter.Instance.OnCorrectDelivery += DeliveryCounter_OnOrderServed;
        DeliveryCounter.Instance.OnWrongDelivery += DeliveryCounter_OnWrongDelivery;
    }

    private void DeliveryCounter_OnOrderServed()
    {
        AudioSource.PlayClipAtPoint(deliveryAccepted, Camera.main.transform.position, 0.5f);
    }

    private void DeliveryCounter_OnWrongDelivery()
    {
        AudioSource.PlayClipAtPoint(deliveryRejected, Camera.main.transform.position, 0.3f);
    }
}
