using UnityEngine;

public class BurnWarningUI : MonoBehaviour
{
    [SerializeField] private FryingPan fryingPan;

    private void Start()
    {
        fryingPan.OnMeatStateChanged += FryingPan_OnMeatStateChanged;
        gameObject.SetActive(false);
    }

    private void FryingPan_OnMeatStateChanged(object sender, FryingPan.OnMeatStateChangedEventArgs e)
    {
        if (fryingPan.GetCurrentState() == FryingPan.State.Burning)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
