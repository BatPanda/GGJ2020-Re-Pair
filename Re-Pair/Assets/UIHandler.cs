﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
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
        if (Input.GetKey(KeyCode.A))
        {
            players[0].score += 10;
        }

        if (Input.GetKey(KeyCode.S))
        {
            players[1].score += 10;
        }

        if (Input.GetKey(KeyCode.D))
        {
            players[2].score += 10;
        }
        if (Input.GetKey(KeyCode.F))
        {
            players[3].score += 10;
        }

        float totalScore = 0;
        int winningPlayer = 0;

        for (int i = 0; i < players.Length; i++)
        {
            totalScore += players[i].score;
            scores[i].GetComponentInChildren<Text>().text = players[i].score.ToString();

            if (players[winningPlayer].score < players[i].score)
            {
                scores[winningPlayer].GetComponentInChildren<Outline>().effectColor = new Color(0, 0, 0, 0);
                winningPlayer = i;
                scores[winningPlayer].GetComponentInChildren<Outline>().effectColor = new Color(0, 0, 0, 1);
            }
        }

        for (int i = 0; i < players.Length; i++)
        {
            scores[i].value = players[i].score / totalScore;
        }
    }
}
