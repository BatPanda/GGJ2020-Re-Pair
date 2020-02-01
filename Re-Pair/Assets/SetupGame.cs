using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetupGame : MonoBehaviour
{
    public GameSettings gameSettings;

    public GameObject[] playerPrefab;

    private int randomPlayerNumber;

    private void Awake()
    {
        Debug.Log(gameSettings.playerSettings.Length);

        if(!gameSettings)
        {
            gameSettings = FindObjectOfType<GameSettings>();
        }

        for (int i = 0; i < gameSettings.playerSettings.Length; i++)
        {
            randomPlayerNumber = Random.Range(0, playerPrefab.Length);

            if(gameSettings.playerSettings[i].connected)
            {
                Instantiate(playerPrefab[randomPlayerNumber]).GetComponent<PlayerController>().controllerNumber = gameSettings.playerSettings[i].playerNum;
            }
        }
    }
}
