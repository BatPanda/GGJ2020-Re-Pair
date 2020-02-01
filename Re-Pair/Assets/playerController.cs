 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public int controllerNum;
    public float moveSpeed = 50f;

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector2(transform.position.x + (Input.GetAxis("Horizontal" + controllerNum) * Time.deltaTime * moveSpeed), transform.position.y + (Input.GetAxis("Vertical" + controllerNum) * Time.deltaTime * moveSpeed));
    }
}
