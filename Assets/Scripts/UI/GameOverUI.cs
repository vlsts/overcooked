using System;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI ordersDeliveredText;
    [SerializeField] private Button mainMenuButton;

    void Start()
    {
        mainMenuButton.onClick.AddListener(() =>
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenuScene");
        });
        gameObject.SetActive(false);
        GameLogicManager.Instance.OnGameEnd += GameLogicManager_OnGameEnd;
    }

    private void GameLogicManager_OnGameEnd(object sender, GameLogicManager.OnGameEndedEventArgs e)
    {
        ordersDeliveredText.text = e.ordersDelivered.ToString();
        gameObject.SetActive(true);
    }
}
