using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    private const string SHOW_BUTTON = "ShowState";

    [SerializeField] private Transform container;
    [SerializeField] private Transform template;
    [SerializeField] private Button backButton;

    private void Awake()
    {
        template.gameObject.SetActive(false);
        backButton.onClick.AddListener(() =>
        {
            ShopManager.Instance.ChangeMode(ShopManager.Mode.None);
            ShopNavigatorUI.Instance.ShowUIOnBack();
            gameObject.SetActive(false);
        });
    }

    private void Start()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container)
        {
            if (child == template) continue;
            Destroy(child.gameObject);
        }
        foreach (ShopItemSO shopItemSO in ShopManager.Instance.GetItemSOList())
        {
            Transform counterTransform = Instantiate(template, container);
            counterTransform.gameObject.SetActive(true);
            counterTransform.GetComponent<ShopSingleUI>().SetCounterIcon(shopItemSO);
        }
        container.GetComponent<Animator>().SetTrigger(SHOW_BUTTON);
        backButton.GetComponent<Animator>().SetTrigger(SHOW_BUTTON);
    }
}
