using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter parentCounter;

    private AudioSource stoveFlame;

    private void Awake()
    {
        stoveFlame = GetComponent<AudioSource>();
        parentCounter.OnStoveStateChanged += StoveCounter_OnStoveStateChanged; ;
    }

    private void StoveCounter_OnStoveStateChanged(bool isOn)
    {
        if (isOn)
        {
            stoveFlame.Play();
        }
        else
        {
            stoveFlame.Stop();
        }
    }
}
