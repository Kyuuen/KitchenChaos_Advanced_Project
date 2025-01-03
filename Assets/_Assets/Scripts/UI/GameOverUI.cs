using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : BaseNavigateUI
{
    [SerializeField] private TextMeshProUGUI profitMadeText;

    [SerializeField] private Button restartButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        GameManager.Instance.OnGameOver += GameManager_OnGameOver; ;
        restartButton.onClick.AddListener(() =>
        {
            LoadGameScene(State.LoadGame);
        });
        shopButton.onClick.AddListener(() =>
        {
            LoadGameScene(State.Shop);
        });
        mainMenuButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.Scene.MainMenuScene);
        });
        Hide();
    }

    private void GameManager_OnGameOver(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.IsLevelPassed())
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
        StartCoroutine(CountUpToTarget());
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    IEnumerator CountUpToTarget()
    {
        int profitMade = MoneyManager.Instance.GetProfit();
        int currentDisplay = 0;
        while (currentDisplay < profitMade)
        {
            currentDisplay += Random.Range(1, 5); // or whatever to get the speed you like
            if(currentDisplay > profitMade) currentDisplay = profitMade;
            profitMadeText.text = "$" + currentDisplay;
            yield return null;
        }
        if (profitMade <= 0)
        {
            profitMadeText.text = "$" + 0;
        }
    }
}
