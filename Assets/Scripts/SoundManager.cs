using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip deliveryAccepted;
    [SerializeField] private AudioClip deliveryRejected;
    [SerializeField] private AudioClip cuttingSound;
    [SerializeField] private AudioClip pickDropItem;

    private AudioSource audioSource;

    public static SoundManager Instance { get; private set; }


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else Destroy(Instance);

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        DeliveryCounter.Instance.OnCorrectDelivery += DeliveryCounter_OnOrderServed;
        DeliveryCounter.Instance.OnWrongDelivery += DeliveryCounter_OnWrongDelivery;
        CuttingCounter.OnCut += CuttingCounter_OnCut;
        Player.Instance.OnPickDropItem += Instance_OnPickDropItem;
        Plate.OnFoodItemAdded += Plate_OnFoodItemAdded;
        FryingPan.OnMeatAdded += FryingPan_OnMeatAdded;
        TrashCounter.OnTrash += TrashCounter_OnTrash;
    }

    private void TrashCounter_OnTrash(object sender, EventArgs e)
    {
        PlaySound(pickDropItem, (sender as TrashCounter).transform.position, 1.0f);
    }

    private void FryingPan_OnMeatAdded(object sender, EventArgs e)
    {
        PlaySound(pickDropItem, (sender as FryingPan).transform.position, 1.0f);
    }

    private void Plate_OnFoodItemAdded(object sender, EventArgs e)
    {
        PlaySound(pickDropItem, (sender as Plate).transform.position, 1.0f);
    }

    private void Instance_OnPickDropItem()
    {
        PlaySound(pickDropItem, Player.Instance.transform.position, 1.0f);
    }

    private void CuttingCounter_OnCut(object sender, EventArgs e)
    {
        PlaySound(cuttingSound, (sender as CuttingCounter).transform.position, 0.7f, 1.5f);
    }

    private void DeliveryCounter_OnOrderServed()
    {
        PlaySound(deliveryAccepted, DeliveryCounter.Instance.transform.position, 0.5f);
    }

    private void DeliveryCounter_OnWrongDelivery()
    {
        PlaySound(deliveryRejected, DeliveryCounter.Instance.transform.position, 0.3f);
    }

    private void PlaySound(AudioClip clip, Vector3 position, float volume, float pitch = 1.0f)
    {
        audioSource.clip = clip;
        audioSource.transform.position = position;
        audioSource.volume = volume;
        audioSource.pitch = pitch;
        audioSource.Play();
    }
}
