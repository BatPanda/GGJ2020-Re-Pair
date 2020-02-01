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

    void Start()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
        MovementManager();
    }

    void MovementManager()
    {

        //handles player movement and adds the news string onto the input name.
        float moveHorizontal = Input.GetAxisRaw("Horizontal" + controllerNumber);
        float moveVertical = Input.GetAxisRaw("Vertical" +  controllerNumber);

        Debug.Log(moveHorizontal);
        if(canMove)
        {
            transform.position += new Vector3(movementSpeed * moveHorizontal, movementSpeed * moveVertical, 0) * Time.deltaTime;
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
}
