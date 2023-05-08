using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoulieAnim : MonoBehaviour
{
    public Rope[] ropeRight;
    public Rope[] ropeLeft;
    public WheelAnimations[] wheels;

    public bool activateLeft = false;
    public bool activateRight = false;

    public float minTime = -5;
    public float maxTime = 5;
    public float moveSpeed = 5;
    public float currTime = 0;
    public void SetActivateLeft(bool activate) {
        foreach (var wa in wheels) {
            wa.rotateLeft = activate;
        }

        activateLeft = activate;
    }
    public void SetActivateRight(bool activate) {
        foreach (var wa in wheels) {
            wa.rotateRight = activate;
        }

        activateRight = activate;
    }


    public void Update() {
        float actualMoveSpeed = 0;
        if (activateLeft != activateRight) {
            if (activateLeft) {
                currTime -= Time.deltaTime;

            }
            else {
                currTime += Time.deltaTime;
            }

            currTime = Mathf.Clamp(currTime, minTime, maxTime);

            if (currTime > minTime && currTime < maxTime) {
                if (activateLeft) {
                    actualMoveSpeed = -moveSpeed;
                }
                else {
                    actualMoveSpeed = moveSpeed;
                }
            }
        }
        foreach (var rope in ropeLeft) {
            rope.SetSize(-currTime * moveSpeed, -actualMoveSpeed);
        }
        foreach (var rope in ropeRight) {
            rope.SetSize(currTime * moveSpeed, actualMoveSpeed);
        }
    }
}