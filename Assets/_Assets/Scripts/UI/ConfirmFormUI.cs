using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmFormUI : BaseNavigateUI
{
    
    [SerializeField] private Button continueButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button returnButton;

    private void Awake()
    {
        continueButton.onClick.RemoveAllListeners();
        newGameButton.onClick.RemoveAllListeners();
        returnButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(() =>
        {
            LoadGameScene(State.LoadGame);
        });
        newGameButton.onClick.AddListener(() =>
        {
            LoadGameScene(State.NewGame);
        });
        returnButton.onClick.AddListener(() =>
        {
            gameObject.SetActive(false);
        });
        
    }
}
