using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotlightScript : MonoBehaviour
{

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SpriteRenderer[] sprites = collision.GetComponentsInChildren<SpriteRenderer>();

            foreach(SpriteRenderer sprite in sprites)
            {
                if(sprite.gameObject.transform.parent != null)
                {
                    sprite.enabled = true;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            SpriteRenderer[] sprites = collision.GetComponentsInChildren<SpriteRenderer>();

            foreach (SpriteRenderer sprite in sprites)
            {
                if (sprite.gameObject.transform.parent != null)
                {
                    sprite.enabled = false;
                }
            }
        }
    }
}
