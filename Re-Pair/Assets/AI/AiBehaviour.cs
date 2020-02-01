using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiBehaviour : MonoBehaviour
{
    private float time;

    private float maxWaitTime = 5;
    private float minWaitTime = 1;

    private float waitingTime = 0;

    private Vector2 destination;
    private Vector2 destination2;
    private bool dest2 = false;

    private bool isMoving = false;

    private Vector3[] heatmap;
    private float totalWeights = 0;

    private GameObject danceFloor;

    private float speed;
    private float maxDistanceToGoal;

    private bool dancing = false;
    private float longestDist;

    private float lowSpeed;
    private float highSpeed;

    public bool canMove = true;


    public void s_AiBehaviour(Vector3[] heatmapToUse, float totalWeightsToUse, float lowestSpeed, float highestSpeed, float maxDistanceToGoalToUse, Vector2 pos, GameObject danceFloor1, float waitingTimeMin, float waitingTimeMax, float longestDistance)
    {
        heatmap = heatmapToUse;
        totalWeights = totalWeightsToUse;
        lowSpeed = lowestSpeed;
        highSpeed = highestSpeed;
        maxDistanceToGoal = maxDistanceToGoalToUse;
        transform.position = pos;
        danceFloor = danceFloor1;
        minWaitTime = waitingTimeMin;
        maxWaitTime = waitingTimeMax;
        longestDist = longestDistance;
    }

    // Start is called before the first frame update
    void Start()
    {
        waitingTime = Random.Range(minWaitTime, maxWaitTime);
    }

    private void FixedUpdate()
    {
        if (canMove)
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
                    dancing = false;
                    isMoving = true;
                    getDestination();
                    speed = Random.Range(lowSpeed, highSpeed);
                    time = 0;
                    GetComponent<Animator>().SetBool("isDancing", false);
                    GetComponent<Animator>().SetBool("isWalking", true);
                }
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
           
            GetComponent<Animator>().SetBool("isWalking", false);
            waitingTime = Random.Range(minWaitTime, maxWaitTime);

            if (waitingTime <= 0 || dest2)
            {
                waitingTime = 0;
            }
            Vector2 danceFloorPos = danceFloor.transform.position;
            Vector2 danceFloorScale = new Vector2(danceFloor.transform.localScale.x, danceFloor.transform.localScale.y);

            if (transform.position.x >= danceFloorPos.x - 2.5f && transform.position.x <= danceFloorPos.x + 2.5f
                && transform.position.y <= danceFloorPos.y + 1.5f && transform.position.y >= danceFloorPos.y - 1.5f)
            {
                GetComponent<Animator>().SetBool("isDancing", true);
                dancing = true;
                if (waitingTime < 1.5f)
                {
                    waitingTime = 2;
                }
                waitingTime *= 1.5f;
            }
        }
    }

    private void getDestination()
    {
        if (dest2)
        {
            destination = destination2;
            dest2 = false;
            return;
        }

        Vector2 thisPos = transform.position;
        Vector2 thischoice;
        Vector2 danceHigh = new Vector2(danceFloor.transform.position.x + 2.5f, danceFloor.transform.position.y + 1.5f);
        Vector2 danceLow = new Vector2(danceFloor.transform.position.x - 2.5f, danceFloor.transform.position.y - 1.5f);


        float choice = Random.Range(0, totalWeights);
        float q = 0;

        for (int i = 0; i < heatmap.Length; i++)
        {
            q += heatmap[i].z;
           
            if (q >= choice)
            {
                
                if ((thisPos.x - heatmap[i].x) == 0)
                {
                    destination = new Vector2(heatmap[i].x, heatmap[i].y);
                    return;
                }
                float a = (thisPos.y - heatmap[i].y) / (thisPos.x - heatmap[i].x);
                float c = thisPos.y - thisPos.x * a;

                float y = danceLow.x * a + c;
                if (y >= danceLow.y && y <= danceHigh.y && ((danceLow.x >= heatmap[i].x && danceLow.x <= thisPos.x) || (danceLow.x <= heatmap[i].x && danceLow.x >= thisPos.x)))
                {
                    destination = findSide(danceHigh, danceLow, a,new Vector2(danceLow.x, y));
                    destination2 = new Vector2(heatmap[i].x, heatmap[i].y);
                    dest2 = true;
                    return;
                }

                y = danceHigh.x * a + c;
                if (y >= danceLow.y && y <= danceHigh.y && ((danceLow.x >= heatmap[i].x && danceLow.x <= thisPos.x) || (danceLow.x <= heatmap[i].x && danceLow.x >= thisPos.x)))
                {
                    destination = findSide(danceHigh, danceLow, a, new Vector2(danceHigh.x, y));
                    destination2 = new Vector2(heatmap[i].x, heatmap[i].y);
                    dest2 = true;
                    return;
                }

                destination = new Vector2(heatmap[i].x, heatmap[i].y);
                return;
            }
        }
    }

    private Vector2 findSide(Vector2 highside, Vector2 lowside, float a, Vector2 positionOfHit)
    {
        if (positionOfHit.x == highside.x)
        {
            if (a > 0)
            {
                return new Vector2(highside.x, lowside.y);
            }
            return new Vector2(highside.x, highside.y);
        }
        else
        {
            if (a > 0)
            {
                return new Vector2(lowside.x, highside.y);
            }
            return new Vector2(lowside.x, lowside.y);
        }
        /*
        else if (positionOfHit.y == highside.y)
        {
            if (a < 0)
            {
                return new Vector2(lowside.x, highside.y);
            }
            return new Vector2(highside.x, highside.y);
        }
        else
        {
            if (a < 0)
            {
                return new Vector2(lowside.x, lowside.y);
            }
            return new Vector2(highside.x, lowside.y);
        }*/
    }
}

