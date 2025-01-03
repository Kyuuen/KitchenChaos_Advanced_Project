using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseNavigateUI : MonoBehaviour
{
    private const string GAME_SCENE_STATE = "GameSceneState";
    public enum State
    {
        LoadGame,
        NewGame,
        Shop
    }

    public void LoadGameScene(State state)
    {
        if(state == State.NewGame) PlayerPrefs.DeleteAll();
        Loader.Load(Loader.Scene.GameScene);
        PlayerPrefs.SetInt(GAME_SCENE_STATE, ((int)state));
    }
}
