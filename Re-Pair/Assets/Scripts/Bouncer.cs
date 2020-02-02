using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public GameObject door;

    private Vector2 startPos;
    private GameObject target;

    private bool charging = false;
    private bool returning = false;

    public float moveSpeed = 0.1f;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if(returning)
        {
            transform.position = Vector2.MoveTowards(transform.position, startPos, moveSpeed * Time.deltaTime);
            
            if(Vector2.Distance(transform.position, startPos) < 0.2f)
            {
                returning = false;
            }
        }

        if (charging)
        {
            if (Vector2.Distance(transform.position, target.transform.position) > 0.2f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                FindObjectOfType<GameSettings>().playerSettings[
                    FindObjectOfType<GameSettings>().FindPlayerNumberByController
                        (
                        target.GetComponent<PlayerController>().controllerNumber
                        )
                    ].alive = false;
                target.GetComponent<PlayerController>().enabled = false;
                target.GetComponent<Collider2D>().enabled = false;
                target.GetComponent<Renderer>().enabled = false;
                charging = false;
                returning = true;
                target = null;
            }
        }
    }

    public void StartBouncer(Collider2D collision)
    {
        door.GetComponent<Animator>().SetTrigger("openDoor");

        target = collision.gameObject;
        charging = true;
        returning = false;
    }
}
