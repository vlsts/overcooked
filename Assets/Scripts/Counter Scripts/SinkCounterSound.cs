using UnityEngine;

public class SinkCounterSound : MonoBehaviour
{
    [SerializeField] private SinkCounter parentCounter;

    private AudioSource washingDishes;

    private void Awake()
    {
        washingDishes = GetComponent<AudioSource>();
        parentCounter.OnTapToggle += SinkCounter_OnTapToggle;
    }

    private void SinkCounter_OnTapToggle(bool isWashing)
    {
        if (isWashing)
        {
            washingDishes.Play();
        }
        else
        {
            washingDishes.Stop();
        }
    }
}
