using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIHandler : MonoBehaviour
{

    public Slider[] scores;
    public Animator[] animators;
    public GameObject[] particles;

    PlayerController[] players;
    MusicManager musicManager;
    GameSettings gameSettings;

    [SerializeField]
    private int winningPlayer = -1;

    private void Awake()
    {
        scores = gameObject.GetComponentsInChildren<Slider>();
        animators = gameObject.GetComponentsInChildren<Animator>();
        players = FindObjectsOfType<PlayerController>();
        musicManager = FindObjectOfType<MusicManager>();
        gameSettings = FindObjectOfType<GameSettings>();

        for (int i = 0; i < scores.Length; i++)
        {
            scores[i].value = 0;
            scores[i].GetComponentInChildren<Outline>().effectColor = new Color(0, 0, 0, 0);
            scores[i].gameObject.SetActive(false);

            animators[i].gameObject.SetActive(false);
            particles[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < players.Length; i++)
        {
            scores[i].gameObject.SetActive(true);
            animators[i].gameObject.SetActive(true);
            particles[i].gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        float totalScore = 0;
        updateOutline();
        updateMusicPlayingUI();

        for (int i = 0; i < players.Length; i++)
        {
            totalScore += players[i].score;
            scores[i].GetComponentInChildren<Text>().text = players[i].score.ToString();
        }

        if (totalScore > 0)
        {
            for (int i = 0; i < players.Length; i++)
            {
                scores[i].value = players[i].score / totalScore;
            }
        }
        
    }

    void updateOutline()
    {
        int winningScore = 0;

        for (int i = 0; i < players.Length; i++)
        {
            if (winningScore < players[i].score)
            {
                winningPlayer = i;
            }
        }

        for (int i = 0; i < players.Length; i++)
        {
            if (i == winningPlayer)
            {
                scores[i].GetComponentInChildren<Outline>().effectColor = new Color(0, 0, 0, 1);
            }
            else
            {
                scores[i].GetComponentInChildren<Outline>().effectColor = new Color(0, 0, 0, 0);
            }
        }
    }

    public void IncreaseScore(int player, int increase)
    {
        players[player].score += increase;

        for (int i = 0; i < players.Length; i++)
        {
            if (i == player)
            {
                particles[i].SetActive(true);
                Vector3 position = particles[i].transform.position;

                if (i == 0 || i == 2)
                {
                    position.x = scores[i].transform.position.x - 3.75f + (scores[i].value * 4);
                }
                else
                {
                    position.x = scores[i].transform.position.x - (scores[i].value * 4);
                }
                
                particles[i].transform.position = position;
            }
            else
            {
                particles[i].SetActive(false);
            }
        }
    }

    void updateMusicPlayingUI()
    {
        int musicPlaying = musicManager.MusicPlaying();

        for (int i = 0; i < players.Length; i++)
        {
            if (gameSettings.playerSettings[i].musicSelected == musicManager.MusicPlaying())
            {
                animators[i].SetBool("music_playing", true);
            }
            else
            {
                animators[i].SetBool("music_playing", false);
            }
        }
    }

    public int GetWinningPlayer()
    {
        return winningPlayer;
    }
}
