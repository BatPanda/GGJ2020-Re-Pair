using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluetoothConnectionHandler : MonoBehaviour
{
    MusicManager musicManager;

    GameObject playerConnecting;
    bool playerInRange = false;

    float connectionTimeDelay = 5f;
    float connectionTimer = 0;

    private void Awake()
    {
        musicManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<MusicManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            playerConnecting = collision.gameObject;
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == playerConnecting)
        {
            playerConnecting = null;
            playerInRange = false;
        }
    }

    private void Update()
    {
        if (playerInRange)
        {
            connectionTimer += Time.deltaTime;

            if (connectionTimer >= connectionTimeDelay)
            {
                musicManager.ChangeMusic(playerConnecting.GetComponent<PlayerController>().getPlayerNumber());
                connectionTimer = 0;
                playerConnecting = null;
                playerInRange = false;
            }
        }
    }
}
