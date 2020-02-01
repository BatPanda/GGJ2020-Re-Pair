using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dancefloor : MonoBehaviour
{
    private GameSettings gameSettings;
    private MusicManager musicManager;
    private ScoreUIHandler uiHandler;

    private void Awake()
    {
        gameSettings = FindObjectOfType<GameSettings>();
        musicManager = FindObjectOfType<MusicManager>();
        uiHandler = FindObjectOfType<ScoreUIHandler>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            collision.GetComponent<Animator>().SetBool("isDancing", true);
            int playerID = gameSettings.FindPlayerNumberByController(collision.GetComponent<PlayerController>().controllerNumber);
            if ( gameSettings.playerSettings[playerID].musicSelected == musicManager.MusicPlaying())
            {
                int score = Mathf.FloorToInt((3 - Vector2.Distance(transform.position, collision.transform.position))*5);
                uiHandler.IncreaseScore(playerID, score);
                uiHandler.particles[playerID].SetActive(true);
            }
            else
            {
                uiHandler.particles[playerID].SetActive(false);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerController>())
        {
            collision.GetComponent<Animator>().SetBool("isDancing", false);
            int playerID = gameSettings.FindPlayerNumberByController(collision.GetComponent<PlayerController>().controllerNumber);
            uiHandler.particles[playerID].SetActive(false);
        }
    }
}
