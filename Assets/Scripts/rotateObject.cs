using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    void Update()
    {
        float ySpeed = -5f * Time.deltaTime;
        transform.Rotate(0, ySpeed, 0);
    }
}
