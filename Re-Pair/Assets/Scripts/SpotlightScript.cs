using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightScript : MonoBehaviour
{
    GameSettings gameSettings;

    PlayerController[] players;

    AiBehaviour[] AICharacters;

    private void Start()
    {
        gameSettings = FindObjectOfType<GameSettings>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && FindObjectOfType<ChooseCharacterScript>().GetCharacterUsing() != -1)
        {
            collision.GetComponent<SpriteRenderer>().color = gameSettings.playerSettings[FindObjectOfType<ChooseCharacterScript>().GetCharacterUsing()].playerColor;
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
}
