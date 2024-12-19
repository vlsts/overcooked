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

    private int ordersDelivered = 0;
    private const float gameplayTime = 10f;
    private float currentTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(Instance);

        currentTime = gameplayTime;
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
            Time.timeScale = 0;
        }
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
