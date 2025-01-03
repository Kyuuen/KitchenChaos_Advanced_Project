using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : BaseNavigateUI
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Transform confirmFormUI;

    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            if (SaveLoadSystem.Instance.IsSaveExist())
            {
                confirmFormUI.gameObject.SetActive(true);
            }
            else
            {
                LoadGameScene(State.NewGame);
            }
        });
        quitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        Time.timeScale = 1.0f;
    }
    private void Start()
    {
        confirmFormUI.gameObject.SetActive(false);
    }
}
