using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    AudioClip player_one_music;
    AudioClip player_two_music;
    AudioClip player_three_music;
    AudioClip player_four_music;

    int musicPlaying;

    public void ChangeMusic(int playerNumber)
    {
        Debug.Log("Changing Music to: " + playerNumber);
        if (playerNumber == 1)
        {
            // Play Player One Music
            musicPlaying = playerNumber;
        }
        else if (playerNumber == 2)
        {
            // Play Player Two Music
            musicPlaying = playerNumber;
        }
        else if (playerNumber == 3)
        {
            // Play Player Three Music
            musicPlaying = playerNumber;
        }
        else if (playerNumber == 4)
        {
            // Play Player our Music
            musicPlaying = playerNumber;
        }
    }

    public int MusicPlaying()
    {
        return musicPlaying;
    }
}
