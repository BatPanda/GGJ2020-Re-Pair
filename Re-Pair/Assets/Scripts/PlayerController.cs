using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float movementSpeed = 4.0f;

    public enum players {player_1, player_2, player_3, player_4};

    public players playerNumber = players.player_1;

    void Start()
    {

    }


    void Update()
    {
        MovementManager(playerNumber);
    }

    void MovementManager(players playerNo)
    {
        string[] splitString = playerNumber.ToString().Split('_');

        float moveHorizontal = Input.GetAxisRaw("Horizontal" + splitString[1].ToString());
        float moveVertical = Input.GetAxisRaw("Vertical" +  splitString[1].ToString());

        transform.position += new Vector3(movementSpeed * moveHorizontal, movementSpeed * moveVertical, 0) * Time.deltaTime;

    }

    public players getPlayerNumber()
    {
        return playerNumber;
    }
}
