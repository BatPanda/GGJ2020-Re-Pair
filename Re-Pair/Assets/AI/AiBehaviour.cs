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

    private bool isMoving = false;

    private Vector3[] heatmap;
    private float totalWeights = 0;

    private GameObject danceFloor;

    private float speed;
    private float maxDistanceToGoal;

    public bool canMove = true;
    public bool dancing = false;


    public void s_AiBehaviour(Vector3[] heatmapToUse, float totalWeightsToUse, float speedToUse, float maxDistanceToGoalToUse, Vector2 pos, GameObject danceFloor1, float waitingTimeMin, float waitingTimeMax)
    {
        heatmap = heatmapToUse;
        totalWeights = totalWeightsToUse;
        speed = speedToUse;
        maxDistanceToGoal = maxDistanceToGoalToUse;
        transform.position = pos;
        danceFloor = danceFloor1;
        minWaitTime = waitingTimeMin;
        maxWaitTime = waitingTimeMax;
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
                    destination = getDestination();
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

            if (waitingTime <= 0)
            {
                waitingTime = 0;
            }
            Vector2 danceFloorPos = danceFloor.transform.position;
            Vector2 danceFloorScale = new Vector2(danceFloor.transform.localScale.x, danceFloor.transform.localScale.y);

            if (transform.position.x >= danceFloorPos.x - danceFloorScale.x/2 && transform.position.x <= danceFloorPos.x + danceFloorScale.x/ 2
                && transform.position.y <= danceFloorPos.y + danceFloorScale.y/ 2 && transform.position.y >= danceFloorPos.y - danceFloorScale.y / 2)
            {
                GetComponent<Animator>().SetBool("isDancing", true);
                dancing = true;
                waitingTime *= 1.5f;
            }
        }
    }

    Vector2 getDestination()
    {
        float choice = Random.Range(0, totalWeights);
        float x = 0;

        for (int i = 0; i < heatmap.Length; i++)
        {
            x += heatmap[i].z;
            if (x >= choice)
            {
                return new Vector2(heatmap[i].x, heatmap[i].y);
            }
        }
        Debug.LogError(x);
       return new Vector2(0, 0);
    }
}
