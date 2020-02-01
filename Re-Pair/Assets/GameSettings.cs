﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    [System.Serializable]
    public struct PlayerSettings
    {
        [HideInInspector]
        public int playerNum;

        public bool connected;
        public int musicSelected;
    }
    public PlayerSettings[] playerSettings = new PlayerSettings[4];

    private void Awake()
    {
        if (FindObjectsOfType<GameSettings>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        for (int i = 0; i<playerSettings.Length; i++)
        {
            playerSettings[i].playerNum = i;
        }
    }

    private void Update()
    {

    }
}
