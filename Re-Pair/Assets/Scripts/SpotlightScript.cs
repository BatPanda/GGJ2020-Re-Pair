using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightScript : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.GetComponent<SpriteRenderer>())
            {
                collision.GetComponent<SpriteRenderer>().color = Color.black;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }
}
