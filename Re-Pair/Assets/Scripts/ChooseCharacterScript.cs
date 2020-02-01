using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacterScript : MonoBehaviour
{
    public GameObject spotlight;

    private int playerUsing = -1;

    private bool timeIsStopped = false;

    public float spotLightMoveSpeed = 5.0f;

    public float maxPositionX = 10;
    public float maxPositionY = 4;

    public float minPositionX = -10;
    public float minPositionY = -4;

    private float freezeTime;
    public float startFreezeTime = 10;

  

    private Vector3 originalSpotlightPos;

    private void Start()
    {
        freezeTime = startFreezeTime + Time.time;

        originalSpotlightPos = spotlight.transform.position;
    }


    void Update()
    {
        for(int i = 1; i < 7; i++)
        {
            if(Input.GetAxis("Fire" + i) > 0 && !timeIsStopped)
            {
                timeIsStopped = true;
                for(int j = 0; j<4; j++)
                {
                    if(FindObjectOfType<GameSettings>().playerSettings[j].playerNum == i)
                    {
                        playerUsing = j;
                    }
                }

                CharactersCanMove(true);
                break;
            }

            if(timeIsStopped && i == FindObjectOfType<GameSettings>().playerSettings[playerUsing].playerNum)
            {
                ManageSpotlightMovement(i);
            }
        }
    }

    void ManageSpotlightMovement(int controllerIndex)
    {
        spotlight.SetActive(true);

        float moveHorizontal = Input.GetAxisRaw("Horizontal" + controllerIndex);
        float moveVertical = Input.GetAxisRaw("Vertical" + controllerIndex);

        if (spotlight.transform.position.x > maxPositionX)
        {
            spotlight.transform.position = new Vector2(maxPositionX, spotlight.transform.position.y);
        }
        else if(spotlight.transform.position.x < minPositionX)
        {
            spotlight.transform.position = new Vector2(minPositionX, spotlight.transform.position.y);
        }

        if (spotlight.transform.position.y > maxPositionY)
        {
            spotlight.transform.position = new Vector2(spotlight.transform.position.x, maxPositionY);
        }
        else if (spotlight.transform.position.y < minPositionY)
        {
            spotlight.transform.position = new Vector2(spotlight.transform.position.x, minPositionY);
        }

        spotlight.transform.position += new Vector3(spotLightMoveSpeed * moveHorizontal, spotLightMoveSpeed * moveVertical, 0) * Time.deltaTime;

        FreezeTimer();
    }

    void CharactersCanMove(bool canMove)
    {
        PlayerController[] players = FindObjectsOfType<PlayerController>();

        foreach(PlayerController player in players)
        {
            player.canMove = canMove;
        }
    }

    void FreezeTimer()
    {
        if(Time.time > freezeTime)
        {
            spotlight.SetActive(false);
            spotlight.transform.position = originalSpotlightPos;
            CharactersCanMove(false);
            timeIsStopped = false;
            freezeTime = startFreezeTime + Time.time;
        }
    }
}
