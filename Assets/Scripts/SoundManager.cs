using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Rendering;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClip deliveryAccepted;
    [SerializeField] private AudioClip deliveryRejected;
    [SerializeField] private AudioClip cuttingSound;
    [SerializeField] private AudioClip pickDropItem;

    public static SoundManager Instance { get; private set; }

    private List<AudioSource> audioSourcePool;
    private const int POOL_SIZE = 10;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);

        audioSourcePool = new List<AudioSource>();

        InitializeAudioSourcePool();
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
        GameLogicManager.Instance.OnGameEnd += GameLogicManager_OnGameEnd;
    }

    private void InitializeAudioSourcePool()
    {
        for (int i = 0; i < POOL_SIZE; i++)
        {
            GameObject audioSourceObject = new GameObject("AudioSource_" + i);
            audioSourceObject.transform.SetParent(transform);
            AudioSource audioSource = audioSourceObject.AddComponent<AudioSource>();
            audioSourcePool.Add(audioSource);
        }
    }

    private AudioSource GetAvailableAudioSource()
    {
        foreach (var audioSource in audioSourcePool)
        {
            if (!audioSource.isPlaying)
                return audioSource;
        }

        return audioSourcePool[0];
    }

    private void GameLogicManager_OnGameEnd(object sender, GameLogicManager.OnGameEndedEventArgs e)
    {
        AudioSource[] audioSources = FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        foreach (AudioSource audioSource in audioSources)
        {
            audioSource.Stop();
        }
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
        AudioSource availableSource = GetAvailableAudioSource();
        availableSource.transform.position = position;
        availableSource.clip = clip;
        availableSource.volume = volume;
        availableSource.pitch = pitch;
        availableSource.Play();
    }
}
