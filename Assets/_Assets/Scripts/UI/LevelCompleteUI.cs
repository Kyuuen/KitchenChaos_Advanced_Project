using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelCompleteUI : BaseNavigateUI
{
    [SerializeField] private TextMeshProUGUI totalProfitText;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button mainMenuButton;

    private void Start()
    {
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
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
        if (GameManager.Instance.IsLevelPassed() && GameManager.Instance.GetCurrentLevel() <= 3)
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
            currentDisplay += Random.Range(1,5); // or whatever to get the speed you like
            if (currentDisplay > profitMade) currentDisplay = profitMade;
            totalProfitText.text = "$" + currentDisplay;
            yield return null;
        }
        if (profitMade <= 0)
        {
            totalProfitText.text = "$" + 0;
        }
    }
}
