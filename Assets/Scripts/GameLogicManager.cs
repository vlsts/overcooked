using System;
using UnityEngine;

public class GameLogicManager : MonoBehaviour
{
    public static GameLogicManager Instance { get; private set; }

    public event EventHandler<OnGameEndedEventArgs> OnGameEnd;
    public class OnGameEndedEventArgs : EventArgs 
    {
        public int ordersDelivered;
    }

    private const float GAMEPLAY_TIME = 5f;
    private int ordersDelivered;
    private float currentTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(Instance);

        ordersDelivered = 0;
        currentTime = GAMEPLAY_TIME;
    }

    private void Start()
    {
        DeliveryCounter.Instance.OnCorrectDelivery += DeliveryCounter_OnCorrectDelivery;
    }

    private void DeliveryCounter_OnCorrectDelivery()
    {
        ordersDelivered++;
    }

    void Update()
    {
        currentTime -= Time.deltaTime;

        if (currentTime <= 0)
        {
            currentTime = 0;

            TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
            OnGameEnd?.Invoke(this, new OnGameEndedEventArgs
            {
                ordersDelivered = ordersDelivered
            });
            ResetStaticEvents();
            Time.timeScale = 0;
        }
    }

    private void ResetStaticEvents()
    {
        CuttingCounter.ResetStatcEvents();
        TrashCounter.ResetStaticEvents();
        FryingPan.ResetStaticEvents();
        Plate.ResetStaticEvents();
    }

    public (int minutes, int seconds) GetCurrentTime()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        return (timeSpan.Minutes, timeSpan.Seconds);
    }

    public string GetCurrentTimeString()
    {
        TimeSpan timeSpan = TimeSpan.FromSeconds(currentTime);
        return $"{timeSpan.Minutes:D2}:{timeSpan.Seconds:D2}";
    }
}
