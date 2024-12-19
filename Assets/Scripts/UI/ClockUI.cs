using UnityEngine;

public class ClockUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI clockText;

    void Update()
    {
        clockText.text = GameLogicManager.Instance.GetCurrentTimeString();
    }
}
