﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MusicManager : MonoBehaviour
{
    private GameSettings gameSettings;
    public AudioSource music;
    public AudioSource pairingFX;

    [SerializeField]
    private List<AudioClip> loadAudio;

    public GameObject danceFloorSprite;

    [SerializeField]
    int musicPlaying;

    private void Awake()
    {
        List<int> musicSelected = new List<int>();
        gameSettings = FindObjectOfType<GameSettings>();
        for(int i=0; i <gameSettings.playerSettings.Length; i++)
        {
            if(!gameSettings.playerSettings[i].connected)
            {
                break;
            }
            musicSelected.Add(gameSettings.playerSettings[i].musicSelected);
        }
        int randomSong = Random.Range(0, 12);
        while(true)
        {
            if(musicSelected.Contains(randomSong))
            {
                randomSong = randomSong == 11 ? 0 : randomSong + 1;
            }
            else
            {
                musicSelected.Add(randomSong);
                break;
            }
        }

        foreach(int songID in musicSelected)
        {
            loadAudio.Add(Resources.Load<AudioClip>("Sounds/DEFAULTSONG" + (songID + 1).ToString()));
        }
        music.clip = loadAudio[loadAudio.Count - 1];
        music.Play();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void ChangeMusic(int playerNumber)
    {
        if (playerNumber != musicPlaying)
        {
            SpriteRenderer danceFloorRenderer = danceFloorSprite.GetComponent<SpriteRenderer>();
            if (playerNumber < 0)
            {
                music.clip = loadAudio[loadAudio.Count - 1];
            }
            else
            {
                pairingFX.Play();
                musicPlaying = gameSettings.playerSettings[playerNumber].musicSelected;
                danceFloorRenderer.color = gameSettings.playerSettings[playerNumber].playerColor;
                music.clip = loadAudio[playerNumber];
            }
            music.Stop();

            music.Play();
        }
    }

    public int MusicPlaying()
    {
        return musicPlaying;
    }
}
