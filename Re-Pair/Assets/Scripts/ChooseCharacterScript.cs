using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseCharacterScript : MonoBehaviour
{
    public GameObject spotlight;

    private bool timeIsStopped = false;

    public float spotLightMoveSpeed = 5.0f;

    public float maxPositionX = 10;
    public float maxPositionY = 4;

    public float minPositionX = -10;
    public float minPositionY = -4;

    private void Start()
    {
        
    }


    void Update()
    {
        for(int i = 1; i < 7; i++)
        {
            if(Input.GetAxis("Fire" + i) > 0)
            {
                timeIsStopped = true;
                break;
            }

            if(timeIsStopped)
            {
                ManageSpotlightMovement(i);
            }
        }
    }

    void ManageSpotlightMovement(int playerIndex)
    {
        spotlight.SetActive(true);

        float moveHorizontal = Input.GetAxisRaw("Horizontal" + playerIndex);
        float moveVertical = Input.GetAxisRaw("Vertical" + playerIndex);

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
    }
}
