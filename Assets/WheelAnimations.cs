using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelAnimations : MonoBehaviour
{
    public bool rotateLeft = false;
    public bool rotateRight = false;

    public float rotationSpeed = 100;

    // Update is called once per frame
    void Update()
    {
        if (rotateLeft != rotateRight) {
            float signedSpeed = rotationSpeed;
            if (rotateRight) {
                signedSpeed *= -1;
            }

            transform.Rotate(Vector3.forward, signedSpeed * Time.deltaTime);
        } 
    }
}
