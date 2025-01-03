using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyText;

    private void Start()
    {
        MoneyManager.Instance.OnMoneyChange += MoneyManager_OnMoneyChange;
        ShopNavigatorUI.Instance.OnModeChange += ShopNavigatorUI_OnModeChange;
        UpdateVisual();
    }

    private void ShopNavigatorUI_OnModeChange(object sender, System.EventArgs e)
    {
        if (ShopManager.Instance.IsArrangeMode())
        {
            gameObject.SetActive(false);
        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    private void MoneyManager_OnMoneyChange(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        moneyText.text = "$" + MoneyManager.Instance.GetCurrentMoney().ToString();
    }
}
