using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter counterObject;
    [SerializeField] private GameObject selectedVisual;

    void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        selectedVisual.SetActive(e.selectedCounter == counterObject);
    }

    protected BaseCounter GetBaseCounter()
    {
        return counterObject;
    }
}
