using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioClip player_one_music;
    AudioClip player_two_music;
    AudioClip player_three_music;
    AudioClip player_four_music;

    PlayerController.players musicPlaying;

    public void ChangeMusic(PlayerController.players playerNumber)
    {
        Debug.Log("Changing Music to: " + playerNumber);
        if (playerNumber == PlayerController.players.player_1)
        {
            // Play Player One Music
            musicPlaying = playerNumber;
        }
        else if (playerNumber == PlayerController.players.player_2)
        {
            // Play Player Two Music
            musicPlaying = playerNumber;
        }
        else if (playerNumber == PlayerController.players.player_3)
        {
            // Play Player Three Music
            musicPlaying = playerNumber;
        }
        else if (playerNumber == PlayerController.players.player_4)
        {
            // Play Player our Music
            musicPlaying = playerNumber;
        }
    }

    public PlayerController.players MusicPlaying()
    {
        return musicPlaying;
    }
}
