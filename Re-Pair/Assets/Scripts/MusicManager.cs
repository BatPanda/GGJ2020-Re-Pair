using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private GameSettings gameSettings;
    public AudioSource music;
    public AudioSource pairingFX;


    [SerializeField]
    int musicPlaying;

    private void Awake()
    {
        gameSettings = FindObjectOfType<GameSettings>();
    }

    public void ChangeMusic(int playerNumber)
    {
        pairingFX.Play();
        musicPlaying = gameSettings.playerSettings[playerNumber].musicSelected;
    }

    public int MusicPlaying()
    {
        return musicPlaying;
    }
}
