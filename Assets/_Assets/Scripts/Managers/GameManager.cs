using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private const string GAME_SCENE_STATE = "GameSceneState";

    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;
    public event EventHandler OnGameOver;
    private enum State
    {
        Shopping,
        WaitingToStart,
        CountingToStart,
        GamePlaying,
        GameOver
    }

    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
        int sceneModeIndex = PlayerPrefs.GetInt(GAME_SCENE_STATE, 0);
        switch (sceneModeIndex)
        {
            case 0: //Player want to continue the current process
                state = State.WaitingToStart;
                currentLevel = SaveLoadSystem.Instance.LoadLevel();
                DeliveryManager.Instance.SetCurrentLevel();
                SaveLoadSystem.Instance.LoadScene();
                break;
            case 1: //Player want to start a new game
                state = State.WaitingToStart;
                currentLevel = SaveLoadSystem.Instance.ResetLevel();
                DeliveryManager.Instance.SetCurrentLevel();
                SaveLoadSystem.Instance.ResetData();
                break;
            case 2: //Switch to shop mode
                state = State.Shopping;
                SaveLoadSystem.Instance.LoadScene();
                break;
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (state == State.WaitingToStart)
        {
            state = State.CountingToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        PauseGameToggle();
    }

    private State state;
    private float countDownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 20f;
    private bool isGamePaused = false;
    private bool isLevelPassed = false;
    private int currentLevel;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        switch (state)
        {
            case State.WaitingToStart:
                break;
            case State.CountingToStart:
                countDownToStartTimer -= Time.deltaTime;
                if (countDownToStartTimer < 0f)
                {
                    state = State.GamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                    gamePlayingTimer = gamePlayingTimerMax;
                }
                break;
            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f)
                {
                    state = State.GameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.GameOver:
                break;
        }
    }

    public bool IsGamePlaying()
    {
        return state == State.GamePlaying;
    }

    public bool IsCountDownToStart()
    {
        return state == State.CountingToStart;
    }

    public float GetCountDownToStartTimer()
    {
        return countDownToStartTimer;
    }

    public bool IsGameOver()
    {
        return state == State.GameOver;
    }
    public bool IsShopping()
    {
        return state == State.Shopping;
    }

    public float GetGamePlayingTimerNormalized()
    {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public void PauseGameToggle()
    {
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0f;
            OnGamePaused?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            Time.timeScale = 1f;
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
        }
    }

    public int GetCurrentLevel()
    {
        return currentLevel;
    }

    public void GetToNextLevel()
    {
        currentLevel++;
        if (currentLevel > 3) return; //Reach last level already
        SaveLoadSystem.Instance.SaveLevel(currentLevel);
    }

    public void SetLevelTimerMax(float timerMax)
    {
        gamePlayingTimerMax = timerMax;
    }

    public void SetLevelStatus(bool isPassed)
    {
        isLevelPassed = isPassed;
        if (isLevelPassed) GetToNextLevel();
        OnGameOver?.Invoke(this, EventArgs.Empty);
    }

    public bool IsLevelPassed()
    {
        return isLevelPassed;
    }
}