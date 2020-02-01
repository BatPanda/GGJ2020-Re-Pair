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
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "AI") && FindObjectOfType<ChooseCharacterScript>().GetCharacterUsing() != -1)
        {
            collision.GetComponent<SpriteRenderer>().color = gameSettings.playerSettings[FindObjectOfType<ChooseCharacterScript>().GetCharacterUsing()].playerColor;

            if (Input.GetButtonDown("Select" + GameObject.FindObjectOfType<ChooseCharacterScript>().playerControlling))
            {
                characterCollider = collision;
                startBouncerAnim = true;
            }
        }

        if(collision.gameObject.tag == "AI")
        {
            collision.GetComponent<SpriteRenderer>().color = gameSettings.playerSettings[FindObjectOfType<ChooseCharacterScript>().GetCharacterUsing()].playerColor;

            if (Input.GetButtonDown("Select" + GameObject.FindObjectOfType<ChooseCharacterScript>().playerControlling))
            {
                characterCollider = collision;
                startBouncerAnim = true;
            }
        }

    }

    private void Update()
    {
        if(startBouncerAnim)
        {
            CheckCharacter(characterCollider);
        }
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

        if (collision.tag == "AI")
        {
            Debug.Log("AI");

            GameObject.FindObjectOfType<Bouncer>().UpdateBouncer(collision);
        }
        else if(collision.tag == "Player")
        {
            Debug.Log("Player");
        }
    }
}
