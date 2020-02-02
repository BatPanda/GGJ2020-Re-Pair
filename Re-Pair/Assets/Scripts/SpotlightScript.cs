using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightScript : MonoBehaviour
{
    GameSettings gameSettings;

    PlayerController[] players;

    AiBehaviour[] AICharacters;

    private bool startBouncerAnim;

    private Collider2D characterCollider;


    private void Start()
    {
        gameSettings = FindObjectOfType<GameSettings>();
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Renderer>().enabled = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "AI") && FindObjectOfType<ChooseCharacterScript>().GetCharacterUsing() != -1)
        {
            collision.GetComponentInChildren<SpriteRenderer>().color = gameSettings.playerSettings[FindObjectOfType<ChooseCharacterScript>().GetCharacterUsing()].playerColor;

            if (Input.GetButtonDown("Select" + GameObject.FindObjectOfType<ChooseCharacterScript>().playerControlling))
            {
                characterCollider = collision;
            }
        }

        if(collision.gameObject.tag == "AI")
        {
            collision.GetComponentInChildren<SpriteRenderer>().color = gameSettings.playerSettings[FindObjectOfType<ChooseCharacterScript>().GetCharacterUsing()].playerColor;

            if (Input.GetButtonDown("Select" + GameObject.FindObjectOfType<ChooseCharacterScript>().playerControlling))
            {
                characterCollider = collision;
            }
        }

        CheckCharacter(characterCollider);

    }

    private void Update()
    {

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        RevertColours(collision);
    }

    public void RevertColours(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "AI")
        {
            collision.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    public void RevertColours()
    {

        players = FindObjectsOfType<PlayerController>();
        AICharacters = FindObjectsOfType<AiBehaviour>();

        foreach (PlayerController player in players)
        {
            player.GetComponent<SpriteRenderer>().color = Color.white;
        }

        foreach(AiBehaviour ai in AICharacters)
        {
            ai.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

    private void CheckCharacter(Collider2D collision)
    {

        if (collision)
        {
            if (collision.tag == "AI")
            {
                int playerNum = FindObjectOfType<ChooseCharacterScript>().GetCharacterUsing();
                foreach (PlayerController player in FindObjectsOfType<PlayerController>())
                {
                    if (player.controllerNumber == gameSettings.playerSettings[playerNum].playerNum)
                    {
                        GameObject.FindObjectOfType<Bouncer>().StartBouncer(player.GetComponent<Collider2D>());
                        characterCollider = null;
                    }
                }
            }
            else if (collision.tag == "Player")
            {
                GameObject.FindObjectOfType<Bouncer>().StartBouncer(collision);
                characterCollider = null;
            }
            FindObjectOfType<ChooseCharacterScript>().EndFreeze();
        }
    }
}
