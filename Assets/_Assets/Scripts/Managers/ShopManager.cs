using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public static ShopManager Instance { get; private set; }

    public enum Mode
    {
        Shop_Mode,
        Arrange_Mode,
        None
    }

    [SerializeField] private List<ShopItemSO> shopItemSOList;

    private Mode mode;
    private List<Button> purchaseButton;
    private Transform selectedCounterType;
    private int selectedCounterPrice;

    private void Awake()
    {
        Instance = this;
        purchaseButton = new List<Button>();
        mode = Mode.None;
    }

    public void PurchaseCounter(Transform newPosition)
    {
        if (MoneyManager.Instance.GetCurrentMoney() >= selectedCounterPrice)
        {
            MoneyManager.Instance.MoneyChange(-selectedCounterPrice);
            Transform newCounter = Instantiate(selectedCounterType);
            newCounter.parent = newPosition.parent;
            newCounter.transform.localPosition = newPosition.localPosition;
            newCounter.transform.rotation = newPosition.rotation;
            Destroy(newPosition.gameObject);
        }
        else
        {
            //Player not have money to purchase the counter
        }
    }

    public void Active()
    {
        gameObject.SetActive(true);
    }

    public void Inactive()
    {
        gameObject.SetActive(false);
    }

    public List<ShopItemSO> GetItemSOList()
    {
        return shopItemSOList;
    }

    public void AssignPurchaseButton(Button button, ShopItemSO shopItemSO)
    {
        purchaseButton.Add(button);
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() =>
        {
            if (Player.Instance.HasKitchenObject()) { Player.Instance.GetKitchenObject().DestroySelf(); }
            KitchenObject.SpawnKitchenObject(shopItemSO.counterSO, Player.Instance);
            selectedCounterType = shopItemSO.counterPrefab;
            selectedCounterPrice = shopItemSO.price;
        });
    }

    public void ChangeMode(Mode mode)
    {
        this.mode = mode;
    }

    public bool IsShopMode()
    {
        return mode == Mode.Shop_Mode;
    }

    public bool IsArrangeMode()
    {
        return mode == Mode.Arrange_Mode;
    }
}
