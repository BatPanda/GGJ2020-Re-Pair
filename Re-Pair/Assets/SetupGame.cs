using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupGame : MonoBehaviour
{
    public GameSettings gameSettings;

    public GameObject playerPrefab;

    private void Awake()
    {
        if(!gameSettings)
        {
            gameSettings = FindObjectOfType<GameSettings>();
        }

        for (int i = 0; i < gameSettings.playerSettings.Length; i++)
        {
            if(gameSettings.playerSettings[i].connected)
            {
                Instantiate(playerPrefab).GetComponent<PlayerController>().controllerNumber = gameSettings.playerSettings[i].playerNum;
            }
        }
    }
}
