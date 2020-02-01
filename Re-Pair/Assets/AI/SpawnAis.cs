using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAis : MonoBehaviour
{
    private Vector3[] heatmap;
    private float totalWeights = 0;

    public GameObject AiPrefab;

    public int nbOfAis;
    
    public GameObject[] speakers;
    public GameObject danceFloor;

    public float danceFloorAvoidRadius;
    public float speakersAvoidRadius;
    public float avoidanceForce;

    public int number_box_for_one;

    public Vector2 screenStartPos;
    public Vector2 screenEndPos;
    public float speed;
    public float maxDistanceToGoal;

    public float minWaitingTime;
    public float maxWaitingTime;

    // Start is called before the first frame update
    void Start()
    {
        int[] size = { (int)(screenEndPos.x - screenStartPos.x), (int)(screenEndPos.y - screenStartPos.y), 0 };
        size[2] = size[0] * size[1] * number_box_for_one * number_box_for_one;

        heatmap = new Vector3[size[2]];

        for (int i = 0; i < size[2]; i++)
        {
            heatmap[i].x = (i % (size[0] * number_box_for_one));
            heatmap[i].x /= number_box_for_one;
            heatmap[i].x += screenStartPos.x + 0.5f / number_box_for_one;
            heatmap[i].y = ((i - (i % (size[0] * number_box_for_one))) / (size[0] * number_box_for_one));
            heatmap[i].y /= number_box_for_one;
            heatmap[i].y += screenStartPos.y + 0.5f / number_box_for_one;


            Vector2 heatmapPos = new Vector2(heatmap[i].x, heatmap[i].y);
            Vector2 vectorWeight = heatmapPos;
            vectorWeight -= new Vector2(danceFloor.transform.position.x, danceFloor.transform.position.y);
            float weight = 1;

            if (vectorWeight.magnitude <= danceFloorAvoidRadius)
            {
                weight -= ((danceFloorAvoidRadius - vectorWeight.magnitude) / danceFloorAvoidRadius) * avoidanceForce;
                if (weight <= 0)
                {
                    weight = 0.01f;
                }
            }

            for (int y = 0; y < speakers.Length; y++)
            {
                Vector2 speakerPos = speakers[y].transform.position;
                Vector2 absDist = heatmapPos - speakerPos;
                if (absDist.magnitude <= speakersAvoidRadius)
                {
                    weight -= ((speakersAvoidRadius - absDist.magnitude) / speakersAvoidRadius) * (avoidanceForce/2);
                }
            }

            heatmap[i].z = weight;
            totalWeights += weight;


/*           Color colour = new Color(1 - weight, weight, 0, 1);

            GameObject go = Instantiate(AiPrefab, new Vector2(heatmap[i].x, heatmap[i].y), Quaternion.identity);
            go.GetComponent<SpriteRenderer>().color = colour;
            go.transform.localScale = new Vector3(1 / (float)number_box_for_one, 1 / (float)number_box_for_one, 1);
            */
        }

        ///
        for (int i = 0; i < nbOfAis; i++)
        {
            Vector2 position = new Vector2(Random.Range(screenStartPos.x, screenEndPos.x), Random.Range(screenStartPos.y, screenEndPos.y));
            GameObject go = Instantiate(AiPrefab, position, Quaternion.identity);
            go.AddComponent<AiBehaviour>().s_AiBehaviour(heatmap, totalWeights, speed, maxDistanceToGoal, position, danceFloor, minWaitingTime, maxWaitingTime);
        }
    }
}
