using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public GameObject door;

    private Vector2 startPos;
    private GameObject[] target = new GameObject[4];
    private int targetNb = 0;

    private bool charging = false;
    private bool returning = false;

    private bool caughtPlayer = false;
    private const float playerSpawnTime = 10;
    private float[] timer = new float[4];

    public float moveSpeed = 0.1f;

    public float playerCarryPosition = -0.05f;

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

                foreach (Transform child in transform)
                {
                    if(child.GetComponent<SpriteRenderer>())
                    {
                        child.GetComponent<SpriteRenderer>().enabled = false;
                    }
                }
            }
        }

        if (charging)
        {
            if (Vector2.Distance(transform.position, target[targetNb].transform.position) > 0.5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, target[targetNb].transform.position, moveSpeed * Time.deltaTime);
            }
            else
            {
                
                FindObjectOfType<GameSettings>().playerSettings[
                    FindObjectOfType<GameSettings>().FindPlayerNumberByController
                        (
                        target[targetNb].GetComponent<PlayerController>().controllerNumber
                        )
                    ].alive = false;
                target[targetNb].GetComponent<PlayerController>().enabled = false;
                target[targetNb].GetComponent<Collider2D>().enabled = false;
                target[targetNb].transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, -90);
                target[targetNb].transform.parent = transform;
                target[targetNb].transform.localPosition = new Vector3(0, playerCarryPosition, 0); 
                charging = false;
                returning = true;

                caughtPlayer = true;
                timer[targetNb] = 0;
            }
        }

        if (caughtPlayer)
        {
            GameObject[] Ais = GameObject.FindGameObjectsWithTag("AI");
           


            for (int i = 0; i < target.Length; i++)
            {
                if (target[i] != null)
                {
                    timer[i] += Time.deltaTime;
                }
                
                if (timer[i] >= playerSpawnTime)
                {
                    int randomAi = Random.Range(0, Ais.Length);
                    Vector2 newPlayerPosition = Ais[randomAi].transform.position;

                    FindObjectOfType<GameSettings>().playerSettings[
                                FindObjectOfType<GameSettings>().FindPlayerNumberByController
                                    (
                                    target[i].GetComponent<PlayerController>().controllerNumber
                                    )
                                ].alive = true;
                    target[i].GetComponent<PlayerController>().enabled = true;
                    target[i].GetComponent<Collider2D>().enabled = true;
                    target[i].GetComponent<Renderer>().enabled = true;

                    target[i].transform.parent = null;

                    target[i].transform.position = newPlayerPosition;

                    target[i].GetComponent<Animator>().runtimeAnimatorController = Ais[randomAi].GetComponent<Animator>().runtimeAnimatorController;

                    target[i].transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, 0);
                    

                    Destroy(Ais[randomAi]);

                    target[i] = null;

                    int caughtPlayerFalse = 0;

                    for (int y = i; y < 3; y++)
                    {
                        if (target[y + 1] != null && target[y] == null)
                        {
                            target[y] = target[y + 1];
                            target[y + 1] = null;
                        }
                        if (target[y] != null)
                        {
                            caughtPlayerFalse++;

                        }
                    }

                    if (caughtPlayerFalse > 1)
                    {
                        caughtPlayer = false;
                    }
                    
                    timer[i] = 0;
                }
            }
            

        }
            
        
    }

    public void StartBouncer(Collider2D collision)
    {
        door.GetComponent<Animator>().SetTrigger("openDoor");

        targetNb = 0;
        while (target[targetNb] != null)
            {
                targetNb++;
            }

        target[targetNb] = collision.gameObject;
        timer[targetNb] = 0;
        charging = true;
        returning = false;
    }
}
