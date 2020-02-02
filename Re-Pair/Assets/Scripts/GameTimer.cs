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
    public GameObject winParticleEffect;

    public GameObject[] Scores;

    private bool gamePlaying = true;
    private float timeElapsed = 0f;

    private int[] rank;

    // Update is called once per frame

    private void Awake()
    {
        playerWinsText.enabled = false;

    }

    void Update()
    {
        if(timeElapsed >= gameTimeSeconds && gamePlaying && !FindObjectOfType<ChooseCharacterScript>().timeIsStopped)
        {
            gamePlaying = false;
            GameObject[] Ais = GameObject.FindGameObjectsWithTag("AI");
            for (int i = 0; i < Ais.Length; i++)
            {
                Ais[i].GetComponent<AiBehaviour>().leaveScreen = true;
            }

            GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
            float[] score = new float[Players.Length];
            rank = new int[Players.Length];
            float highscore = 0;
            int winner = 0;

            for (int i = 0; i < Players.Length; i++)
            {
                score[i] = Players[i].GetComponent<PlayerController>().score;

                rank[i] = Players.Length - i - 1;
                if (score[i] >= highscore)
                {
                    highscore = score[i];
                    winner = i;
                }
            }

            for (int j = Players.Length - 1; j > 0; j--)
            {
                for (int i = 0; i < j; i++)
                {
                    if (score[i] < score[i + 1])
                    {
                        float scorethis = score[i + 1];
                        score[i + 1] = score[i];
                        score[i] = scorethis;

                        int rankthis = rank[i + 1];
                        rank[i + 1] = rank[i];
                        rank[i] = rankthis;
                    }
                }
            }

            Players[winner].GetComponent<PlayerController>().won = true;

            for (int i = 0; i < Players.Length; i++)
            {
                if (i != winner)
                {
                    Players[i].transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, -90);
                }
                Scores[rank[i]].transform.position = new Vector3(-5, 2 - i, 1);
            }

            StartCoroutine(EndGame());
        }
        else if (!paused && gamePlaying)
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
                if (player.controllerNumber == FindObjectOfType<GameSettings>().playerSettings[winningPlayer].playerNum)
                {
                    winner = player.gameObject;
                }
                else
                {
                    player.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                }
            }
        }

        bool spawnedParticles = false;
        while (timer < endGameTimer)
        {
            if (winner)
            {
                winner.transform.position = Vector2.MoveTowards(winner.transform.position, new Vector2(0, 0), Time.deltaTime * 2f);

                if(winner.transform.position == new Vector3(0, 0, winner.transform.position.z) && !spawnedParticles)
                {
                    Instantiate(winParticleEffect, new Vector3(0, -5, 0), Quaternion.identity);
                    spawnedParticles = true;
                }
            }
            timer += Time.deltaTime;
            yield return 0;
        }

        SceneManager.LoadScene(1);
        yield break;
    }

    public void TogglePause()
    {
        paused = !paused;
    }

    public void TriggerWinCanvas()
    {
        gameTimer.enabled = false;

        playerWinsText.enabled = true;

        playerWinsText.text = "PLAYER" + "get player number" + " WINS!";

        //start slider animation.
    }

}
