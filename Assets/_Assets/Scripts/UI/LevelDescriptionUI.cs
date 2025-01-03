using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelDescriptionUI: MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI targetProfitText;
    [SerializeField] private Transform iconContainter;
    [SerializeField] private Transform iconTemplate;
    [SerializeField] private List<LevelIngredientsSO> levelIngredientsSOs;

    private int currentLevel;

    private void Start()
    {
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        currentLevel = GameManager.Instance.GetCurrentLevel();
        if (!GameManager.Instance.IsShopping())
        {
            UpdateVisual();
            Show();
        }
        else
        {
            Hide();
        }
        iconTemplate.gameObject.SetActive(false);
    }

    private void GameManager_OnStateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountDownToStart())
        {
            Hide();
        }
    }

    private void UpdateVisual()
    {
        levelText.text = "Level " + currentLevel;
        targetProfitText.text = "$" + DeliveryManager.Instance.GetTargetProfit();
        SetIngredientsSO();
    }

    private void SetIngredientsSO()
    {
        LevelIngredientsSO levelIngredientsSO = levelIngredientsSOs[currentLevel - 1];
        foreach (Transform child in iconContainter)
        {
            if (child == iconTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kichenObjectSO in levelIngredientsSO.ingredientSOList)
        {
            Transform iconTransform = Instantiate(iconTemplate, iconContainter);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<Image>().sprite = kichenObjectSO.sprite;
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
