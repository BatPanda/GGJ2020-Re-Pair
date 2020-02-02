using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpin : MonoBehaviour
{
    public float spinSpeed = 0.1f;
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, spinSpeed);

    }
}
