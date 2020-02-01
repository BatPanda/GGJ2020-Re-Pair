using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    private GameSettings gameSettings;
    public AudioSource music;
    public AudioSource pairingFX;


    public GameObject danceFloorSprite;

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

        SpriteRenderer danceFloorRenderer = danceFloorSprite.GetComponent<SpriteRenderer>();
        danceFloorRenderer.color = gameSettings.playerSettings[playerNumber].playerColor;
    }

    public int MusicPlaying()
    {
        return musicPlaying;
    }
}
