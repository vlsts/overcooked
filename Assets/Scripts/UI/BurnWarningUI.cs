using UnityEngine;

public class BurnWarningUI : MonoBehaviour
{
    [SerializeField] private FryingPan fryingPan;

    private void Start()
    {
        fryingPan.OnProgressChanged += FryingPan_OnProgressChanged;
        gameObject.SetActive(false);
    }

    private void FryingPan_OnProgressChanged(object sender, IProgressable.OnProgressChangedEventArgs e)
    {

        bool show = (e.currentProgress > 0 && fryingPan.IsBurning());

        if (show)
            gameObject.SetActive(true);
        else gameObject.SetActive(false);
    }
}
