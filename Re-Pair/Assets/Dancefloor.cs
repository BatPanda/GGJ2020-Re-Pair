using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dancefloor : MonoBehaviour
{
    private GameSettings gameSettings;
    private MusicManager musicManager;
    private UIHandler uiHandler;

    private void Awake()
    {
        gameSettings = FindObjectOfType<GameSettings>();
        musicManager = FindObjectOfType<MusicManager>();
        uiHandler = FindObjectOfType<UIHandler>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            int playerID = gameSettings.FindPlayerNumberByController(collision.GetComponent<PlayerController>().controllerNumber);
            if ( gameSettings.playerSettings[playerID].musicSelected == musicManager.MusicPlaying())
            {
                int score = Mathf.FloorToInt((3 - Vector2.Distance(transform.position, collision.transform.position))*5);
                uiHandler.IncreaseScore(playerID, score);
            }
        }
    }
}
