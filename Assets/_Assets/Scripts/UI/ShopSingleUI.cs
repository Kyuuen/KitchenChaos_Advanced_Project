using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private Button purchaseButton;

    public void SetCounterIcon(ShopItemSO shopItemSO)
    {
        nameText.text = shopItemSO.counterSO.objectName;
        priceText.text = "$" + shopItemSO.price.ToString();
        ShopManager.Instance.AssignPurchaseButton(purchaseButton, shopItemSO);
    }
}
