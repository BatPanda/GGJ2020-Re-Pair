using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bouncer : MonoBehaviour
{
    public GameObject door;

    private float moveSpeed = 0.1f;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    public void UpdateBouncer(Collider2D collision)
    {
        door.GetComponent<Animator>().SetTrigger("openDoor");

        if(Vector2.Distance(transform.position, collision.transform.position) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, collision.transform.position, moveSpeed) * Time.deltaTime;
        }
    }
}
