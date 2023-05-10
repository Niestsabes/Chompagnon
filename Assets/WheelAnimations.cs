using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAnimations : MonoBehaviour
{
    public int rotateLeft = 0;
    public int rotateRight = 0;

    public float rotationSpeed = 100;
    public bool blocked = true;

    // Update is called once per frame
    void Update()
    {

        if (rotateLeft != rotateRight && !blocked) {
            float signedSpeed = rotationSpeed;
            if (rotateRight > rotateLeft) {
                signedSpeed *= -1;
            }

            transform.Rotate(Vector3.forward, signedSpeed * Time.deltaTime);
        } 

    }
}
