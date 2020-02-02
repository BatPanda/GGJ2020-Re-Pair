using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float movementSpeed = 4.0f;

    public int controllerNumber;

    public float score = 0;

    private Animator anim;

    public bool canMove = true;

    public bool won = false;

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        if (won)
        {
            hasWon();
        }
        else
        {
            MovementManager();
        }
        
    }

    void MovementManager()
    {

        //handles player movement and adds the news string onto the input name.
        float moveHorizontal = Input.GetAxisRaw("Horizontal" + controllerNumber);
        float moveVertical = Input.GetAxisRaw("Vertical" +  controllerNumber);

        if(canMove)
        {
            transform.position += new Vector3(movementSpeed * moveHorizontal, movementSpeed * moveVertical, 0) * Time.deltaTime;

            Vector2 screenTopleft = Camera.main.ScreenToWorldPoint(new Vector2(0, 0));
            Vector2 screenBottomRight = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

            transform.position = new Vector2(Mathf.Clamp(transform.position.x, screenTopleft.x, screenBottomRight.x), Mathf.Clamp(transform.position.y, screenTopleft.y, screenBottomRight.y));
        }

        //handles animation between walking and idle.
        if(moveVertical != 0 || moveHorizontal != 0)
        {
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }
        
    }

    private void hasWon()
    {
        Vector2 newPosition = transform.position;
        Vector2 toADD = new Vector2(0, 0) - newPosition;
        toADD.Normalize();

        newPosition += toADD * movementSpeed * Time.deltaTime;
        transform.position = newPosition;
    }
}
