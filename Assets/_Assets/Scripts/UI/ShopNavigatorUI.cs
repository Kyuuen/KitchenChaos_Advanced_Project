using System;
using UnityEngine;
using UnityEngine.UI;

public class ShopNavigatorUI : BaseNavigateUI
{
    public static ShopNavigatorUI Instance { get; private set; }
    public event EventHandler OnModeChange;

    private const string SHOW_BUTTON = "ShowState";

    [SerializeField] private Button shopModeButton;
    [SerializeField] private Button arrangeModeButton;
    [SerializeField] private Button goButton;
    [SerializeField] private Transform shopUI;
    [SerializeField] private Transform arrangeUI;

    private void Awake()
    {
        Instance = this;
        shopModeButton.onClick.RemoveAllListeners();
        arrangeModeButton.onClick.RemoveAllListeners();
        shopModeButton.onClick.AddListener(() =>
        {
            shopUI.gameObject.SetActive(true);
            ShopManager.Instance.ChangeMode(ShopManager.Mode.Shop_Mode);
            OnModeChange?.Invoke(this, EventArgs.Empty);
            HideUIOnModeChange();
        });
        arrangeModeButton.onClick.AddListener(() =>
        {
            arrangeUI.gameObject.SetActive(true);
            ShopManager.Instance.ChangeMode(ShopManager.Mode.Arrange_Mode);
            OnModeChange?.Invoke(this, EventArgs.Empty);
            HideUIOnModeChange();
        });
        goButton.onClick.AddListener(() =>
        {
            MoneyManager.Instance.SaveMoney();
            SaveLoadSystem.Instance.SaveScene();
            LoadGameScene(State.LoadGame);
        });
    }

    private void Start()
    {
        if (!GameManager.Instance.IsShopping())
        {
            Hide();
        }
        else
        {
            shopUI.gameObject.SetActive(false);
            arrangeUI.gameObject.SetActive(false);
            RunAnimation();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void HideUIOnModeChange()
    {
        shopModeButton.gameObject.SetActive(false);
        arrangeModeButton.gameObject.SetActive(false);
        goButton.gameObject.SetActive(false);
    }

    public void ShowUIOnBack()
    {
        shopModeButton.gameObject.SetActive(true);
        arrangeModeButton.gameObject.SetActive(true);
        goButton.gameObject.SetActive(true);
        RunAnimation();
        OnModeChange?.Invoke(this, EventArgs.Empty);
    }

    private void RunAnimation()
    {
        shopModeButton.GetComponent<Animator>().SetTrigger(SHOW_BUTTON);
        arrangeModeButton.GetComponent<Animator>().SetTrigger(SHOW_BUTTON);
    }
}
