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

        if(!gameSettings)
        {
            gameSettings = FindObjectOfType<GameSettings>();
        }

        for(int player = 0; player < gameSettings.playerSettings.Length; player++)
        {
            gameSettings.playerSettings[player].alive = true;
        }

        for (int i = 0; i < gameSettings.playerSettings.Length; i++)
        {
            randomPlayerNumber = Random.Range(0, playerPrefab.Length);

            if(gameSettings.playerSettings[i].connected)
            {
                GameObject newPlayer = Instantiate(playerPrefab[randomPlayerNumber]);
                if(!newPlayer.GetComponent<PlayerController>()) newPlayer.AddComponent<PlayerController>();
                newPlayer.GetComponent<PlayerController>().controllerNumber = gameSettings.playerSettings[i].playerNum;
                newPlayer.GetComponent<Collider2D>().isTrigger = false;
                Vector2 topLeft = Camera.main.ScreenToWorldPoint(new Vector2(50,50));
                Vector2 botRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width-50, Screen.height-50));

                newPlayer.transform.position = new Vector2(Random.Range(topLeft.x, botRight.x), Random.Range(topLeft.y, botRight.y));
            }
        }
    }
}
