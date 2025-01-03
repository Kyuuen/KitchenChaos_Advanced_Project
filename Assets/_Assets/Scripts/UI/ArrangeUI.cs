using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrangeUI : MonoBehaviour
{
    private const string SHOW_BUTTON = "ShowState";

    [SerializeField] private Button backButton;

    private void Awake()
    {
        backButton.onClick.AddListener(() => {
            ShopManager.Instance.ChangeMode(ShopManager.Mode.None);
            ShopNavigatorUI.Instance.ShowUIOnBack();
            gameObject.SetActive(false);
        });
        backButton.GetComponent<Animator>().SetTrigger(SHOW_BUTTON);
    }
}
