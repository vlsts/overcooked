using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    [SerializeField] private StoveCounter parentCounter;

    private AudioSource fryingMeat;

    private void Awake()
    {
        fryingMeat = GetComponent<AudioSource>();
        parentCounter.OnStoveStateChanged += StoveCounter_OnStoveStateChanged; ;
    }

    private void StoveCounter_OnStoveStateChanged(bool obj)
    {
        
    }
}
