using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private GameSettings gameSettings;

    [SerializeField]
    int musicPlaying;

    private void Awake()
    {
        gameSettings = FindObjectOfType<GameSettings>();
    }

    public void ChangeMusic(int playerNumber)
    {
        musicPlaying = gameSettings.playerSettings[playerNumber].musicSelected;
    }

    public int MusicPlaying()
    {
        return musicPlaying;
    }
}
