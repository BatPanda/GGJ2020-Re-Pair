using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{

    public GameObject doorParticles;

    public void spawnDoorParticles()
    {
        Instantiate(doorParticles, transform.position, transform.rotation);
    }
}
