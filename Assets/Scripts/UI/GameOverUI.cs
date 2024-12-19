using UnityEngine;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI ordersDeliveredText;

    void Start()
    {
        gameObject.SetActive(false);
        GameLogicManager.Instance.OnGameEnd += GameLogicManager_OnGameEnd;
    }

    private void GameLogicManager_OnGameEnd(object sender, GameLogicManager.OnGameEndedEventArgs e)
    {
        ordersDeliveredText.text = e.ordersDelivered.ToString();
        gameObject.SetActive(true);
    }
}
