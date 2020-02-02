using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public GameObject door;

    private Vector2 startPos;
    private GameObject target;
    private GameObject[] spawning;

    private bool charging = false;
    private bool returning = false;

    private bool caughtPlayer = false;
    private const float playerSpawnTime = 10;
    private float timer = 0;

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
            if (Vector2.Distance(transform.position, target.transform.position) > 0.5f)
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
                target.transform.eulerAngles = new Vector3(transform.rotation.x, transform.rotation.y, -90);
                target.transform.parent = transform;
                target.transform.localPosition = new Vector3(0, playerCarryPosition, 0); 
                charging = false;
                returning = true;

                //spawning = target;
                //target = null;
                caughtPlayer = true;
                timer = 0;
                //Debug.LogError(spawnNb);
            }
        }

        if (caughtPlayer)
        {
            Debug.LogError("got Here1");
            GameObject[] Ais = GameObject.FindGameObjectsWithTag("AI");
            

            timer += Time.deltaTime;
            if (timer >= playerSpawnTime)
             {
                Debug.LogError("got Here2");
                int randomAi = Random.Range(0, Ais.Length);
                Vector2 newPlayerPosition = Ais[randomAi].transform.position;

                FindObjectOfType<GameSettings>().playerSettings[
                            FindObjectOfType<GameSettings>().FindPlayerNumberByController
                                (
                                target.GetComponent<PlayerController>().controllerNumber
                                )
                            ].alive = true;
                target.GetComponent<PlayerController>().enabled = true;
                target.GetComponent<Collider2D>().enabled = true;
                target.GetComponent<Renderer>().enabled = true;

                target.transform.position = newPlayerPosition;

                target.GetComponent<Animator>().runtimeAnimatorController = Ais[randomAi].GetComponent<Animator>().runtimeAnimatorController;





                Destroy(Ais[randomAi]);

                target = null;

                caughtPlayer = false;
                timer = 0;
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
