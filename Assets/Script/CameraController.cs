using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    float inputX, inputZ; 
    // Update is called once per frame
    void Update()
    {
        inputX = Input.GetAxis("Horizontal");
        inputZ = Input.GetAxis("Vertical");

        if (inputX != 0)
        {
            rotate();
        }
        if (inputZ != 0)
        {
            move();
        }
    }

    private void move()
    {
        transform.position += transform.forward * inputZ * Time.deltaTime * 70;

    }

    private void rotate()
    {
        transform.Rotate(new Vector3(0f, inputX * 50 * Time.deltaTime, 0f));
    }
}
