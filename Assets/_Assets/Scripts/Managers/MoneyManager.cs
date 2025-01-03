using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MoneyManager : MonoBehaviour
{
    public static MoneyManager Instance { get; private set; }

    public event EventHandler OnMoneyChange;

    private const string MONEY = "Money";

    private int money;
    private int profit;

    private void Awake()
    {
        Instance = this;
        money = PlayerPrefs.GetInt(MONEY, 0);
    }

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            GameManager.Instance.SetLevelStatus(profit >= DeliveryManager.Instance.GetTargetProfit());
            if (profit >= DeliveryManager.Instance.GetTargetProfit())
            {
                SaveMoney();
            }
        }
        else if (GameManager.Instance.IsCountDownToStart())
        {
            profit = 0;
        }
    }

    public void SaveMoney()
    {
        PlayerPrefs.SetInt(MONEY, money);
    }

    public int GetCurrentMoney()
    {
        return money;
    }

    public void MoneyChange(int changeAmount)
    {
        money += changeAmount;
        profit += changeAmount;
        OnMoneyChange?.Invoke(this, EventArgs.Empty);
    }

    public int GetProfit()
    {
        return profit;
    }
}
