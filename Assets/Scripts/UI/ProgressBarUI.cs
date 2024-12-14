using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject gameObjectWithProgressBar;
    [SerializeField] private Image barImage;

    private IProgressable progressableObject;

    private void Start()
    {
        progressableObject = gameObjectWithProgressBar.GetComponent<IProgressable>();

        if (progressableObject != null)
        {
            progressableObject.OnProgressChanged += Progressable_OnProgressChanged;
            barImage.fillAmount = 0f;
        }
        gameObject.SetActive(false);
    }

    private void Progressable_OnProgressChanged(object sender, IProgressable.OnProgressChangedEventArgs e)
    {
        barImage.fillAmount = e.currentProgress;

        if (e.currentProgress > 0f && e.currentProgress < 1f)
        {
            gameObject.SetActive(true);
        }
        else gameObject.SetActive(false);
    }
}
