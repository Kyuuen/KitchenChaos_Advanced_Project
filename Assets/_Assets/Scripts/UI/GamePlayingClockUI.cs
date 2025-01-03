using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image timerImage;

    private void Start()
    {
        timerImage.fillAmount = 0;
        if(GameManager.Instance.IsShopping()) Hide();
    }

    private void Update()
    {
        timerImage.fillAmount = GameManager.Instance.GetGamePlayingTimerNormalized();
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
