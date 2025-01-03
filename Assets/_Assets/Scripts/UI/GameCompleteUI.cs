using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCompleteUI : MonoBehaviour
{
    private void Start()
    {
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
        Hide();
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsGameOver() && GameManager.Instance.GetCurrentLevel() > 3)
        {
            Loader.Load(Loader.Scene.MainMenuScene);
            SaveLoadSystem.Instance.ResetData();
            SaveLoadSystem.Instance.ResetLevel();
        }
    }

    private void GameManager_OnGameOver(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsLevelPassed() && GameManager.Instance.GetCurrentLevel() > 3)
        {
            Show();
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
