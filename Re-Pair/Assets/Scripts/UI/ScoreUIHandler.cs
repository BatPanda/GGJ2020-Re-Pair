using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUIHandler : MonoBehaviour
{
    Slider[] scores;
    PlayerController[] players;

    private void Awake()
    {
        scores = gameObject.GetComponentsInChildren<Slider>();
        players = FindObjectsOfType<PlayerController>();

        for (int i = 0; i < scores.Length; i++)
        {
            scores[i].value = 0;
            scores[i].GetComponentInChildren<Outline>().effectColor = new Color(0, 0, 0, 0);
            scores[i].gameObject.SetActive(false);
        }

        for (int i = 0; i < players.Length; i++)
        {
            scores[i].gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        float totalScore = 0;
        updateOutline();

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
        int winningPlayer = 0;

        for (int i = 0; i < players.Length; i++)
        {
            if (players[winningPlayer].score < players[i].score)
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
    }
}
