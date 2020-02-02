using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float gameTimeSeconds = 10*4;
    public float endGameTimer = 10f;
    public float panicTime = 30f;

    bool paused = false;

    int panicState = 0;

    public Text gameTimer;

    private bool gamePlaying = true;
    private float timeElapsed = 0f;

    // Update is called once per frame
    void Update()
    {
        if(timeElapsed >= gameTimeSeconds && gamePlaying)
        {
            gamePlaying = false;
            GameObject[] Ais = GameObject.FindGameObjectsWithTag("AI");
            for (int i = 0; i < Ais.Length; i++)
            {
                Ais[i].GetComponent<AiBehaviour>().leaveScreen = true;
            }
            StartCoroutine(EndGame());
        }
        else if (!paused)
        {
            timeElapsed += Time.deltaTime;
            if (gameTimeSeconds - timeElapsed < panicTime && panicState == 0)
            {
                panicState = 1;
                gameTimer.color = Color.red;
                FindObjectOfType<MusicManager>().music.pitch = 1.2f;
            }
            else if(panicState == 1 && gameTimeSeconds - timeElapsed < 10)
            {
                FindObjectOfType<MusicManager>().music.pitch = 1.35f;
            }
            System.TimeSpan timeSpan = System.TimeSpan.FromSeconds(gameTimeSeconds - timeElapsed);
            gameTimer.text = string.Format("{0:D2}:{1:D2}", timeSpan.Minutes, timeSpan.Seconds);
        }
    }

    IEnumerator EndGame()
    {
        int winningPlayer = FindObjectOfType<ScoreUIHandler>().GetWinningPlayer();
        FindObjectOfType<MusicManager>().ChangeMusic(winningPlayer);
        FindObjectOfType<MusicManager>().music.pitch = 1f;
        float timer = 0f;
        PlayerController[] players = FindObjectsOfType<PlayerController>();
        GameObject winner = null;
        if (winningPlayer >= 0)
        {
            foreach (PlayerController player in players)
            {
                player.enabled = false;
                player.GetComponent<Collider2D>().enabled = false;
                if (player.controllerNumber == FindObjectOfType<GameSettings>().playerSettings[winningPlayer].playerNum) winner = player.gameObject;
            }
        }
        while (timer < endGameTimer)
        {
            if (winner)
            {
                winner.transform.position = Vector2.MoveTowards(winner.transform.position, new Vector2(0, 0), Time.deltaTime * 5f);
            }
            timer += Time.deltaTime;
            yield return 0;
        }

        SceneManager.LoadScene(0);
        yield break;
    }

    public void TogglePause()
    {
        paused = !paused;
    }
}
