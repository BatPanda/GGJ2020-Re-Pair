using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBehaviour : MonoBehaviour
{
    private float time;

    private const float maxWaitTime = 5;
    private const float minWaitTime = 1;

    private float waitingTime = 0;

    private Vector2 destination;

    private bool isMoving = false;

    private Vector3[] heatmap;
    private float totalWeights = 0;


    public float maxDistanceToMove;
    public Vector2 screenStratPos;
    public Vector2 screenEndPos;
    public float speed;
    public float maxDistanceToGoal;

    // Start is called before the first frame update
    void Start()
    {
        heatmap = new Vector3[100];

        for(int i = 0; i < 100; i++)
        {
            heatmap[i].x = i % 10 - 5;
            heatmap[i].y = (i - i % 10) / 10 - 5;


            Vector2 weight = new Vector2(heatmap[i].x, heatmap[i].y);
            weight -= new Vector2(0, 0);

            // The weight of the heatmap is set here
            heatmap[i].z = ((weight.magnitude) + 5) / 10;
            totalWeights += heatmap[i].z;
        }

    }

    private void FixedUpdate()
    {
       if (isMoving)
        {
           moveAi();
           checkIfStop();
        }
        else
        {
            time += Time.deltaTime;
            if (time >= waitingTime)
            {
                isMoving = true;
                destination = getDestination();
                time = 0;
            }
        }
    }

    private void moveAi()
    {
        Vector2 newPosition = transform.position;
        Vector2 toADD = destination - newPosition;
        toADD.Normalize();

        newPosition += toADD * speed * Time.deltaTime;
        transform.position = newPosition;
    }

    private void checkIfStop()
    {
        Vector2 distance = transform.position;
        distance -= destination;

        isMoving = distance.magnitude >= maxDistanceToGoal;

        if (!isMoving)
        {
            waitingTime = Random.Range(minWaitTime, maxWaitTime);
        }
    }

    Vector2 getDestination()
    {
        float choice = Random.Range(0, totalWeights);
        float x = 0;

        for (int i = 0; i < 100; i++)
        {
            x += heatmap[i].z;
            if (x >= choice)
            {
                return new Vector2(heatmap[i].x, heatmap[i].y);
            }
        }
       return new Vector2(Random.Range(screenStratPos.x, screenEndPos.x), Random.Range(screenStratPos.y, screenEndPos.y));
    }
}
