using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get; private set; }

    private const string MUSIC_VOLUME = "MusicVolume";

    [SerializeField] private AudioClip[] musics;

    private AudioSource musicSource;
    private float volume = .3f;

    private void Awake()
    {
        Instance = this;
        musicSource = GetComponent<AudioSource>();
        musicSource.clip = musics[0];
        volume = PlayerPrefs.GetFloat(MUSIC_VOLUME, .3f);
        musicSource.volume = volume;
    }

    private void Start()
    {
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
    }

    private void GameManager_OnGameOver(object sender, System.EventArgs e)
    {
        if(GameManager.Instance.IsLevelPassed() && GameManager.Instance.GetCurrentLevel() > 3)
        {
            musicSource.Stop();
            musicSource.clip = musics[1];
            musicSource.Play();
        }
    }

    public void ChangeVolume()
    {
        volume += .1f;
        if (volume > 1f)
        {
            volume = 0f;
        }
        musicSource.volume = volume;
        PlayerPrefs.SetFloat(MUSIC_VOLUME, volume);
        PlayerPrefs.Save();
    }

    public float GetVolume()
    {
        return volume;
    }
}
