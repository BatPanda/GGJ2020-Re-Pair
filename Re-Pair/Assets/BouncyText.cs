﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BouncyText : MonoBehaviour
{
    public float bounceRate;

    private float bounceTimer = 0;
    private bool up = true;

    private Vector2 startPos;
    private void Awake()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        if(bounceTimer >= bounceRate)
        {
            transform.position = new Vector2(transform.position.x, startPos.y + (up ? 10f : 0f));
            up = !up;
            bounceTimer = 0f;
        }
        else
        {
            bounceTimer += Time.deltaTime;
        }
    }

}
