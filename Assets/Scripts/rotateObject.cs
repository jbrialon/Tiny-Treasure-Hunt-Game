using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObject : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float ySpeed = -5f * Time.deltaTime;
        transform.Rotate(0, ySpeed, 0);
    }
}
