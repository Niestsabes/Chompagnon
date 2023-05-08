using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoulieAnim : MonoBehaviour
{

    public AudioClip Ascenseur;
    public Rope[] ropeRight;
    public Rope[] ropeLeft;
    public WheelAnimations[] wheels;

    public int activateLeft = 0;
    public int activateRight = 0;

    public float minTime = -5;
    public float maxTime = 5;
    public float moveSpeed = 5;
    public float currTime = 0;

    public void SetActivateLeft(bool activate) {
        foreach (var wa in wheels) {
            wa.rotateLeft = activate;
        }

        activateLeft += activate ? 1 : -1;

        if (activateLeft != activateRight && currTime > minTime && currTime < maxTime) {
            if (GetComponent<AudioSource>().isPlaying) return;
            GetComponent<AudioSource>().PlayOneShot(Ascenseur);
        }
    }
    public void SetActivateRight(bool activate) {
        foreach (var wa in wheels) {
            wa.rotateRight = activate;
        }

        activateRight += activate ? 1 : -1;

        if (activateLeft != activateRight && currTime > minTime && currTime < maxTime) {
            if (GetComponent<AudioSource>().isPlaying) return;
            GetComponent<AudioSource>().PlayOneShot(Ascenseur);
        }
    }


    public void Update() {
        float actualMoveSpeed = 0;
        bool moveLeft = false;
        if (activateLeft != activateRight) {
            if (activateLeft > activateRight) {
                currTime -= Time.deltaTime;
                moveLeft = true;

            }
            else {
                currTime += Time.deltaTime;
                moveLeft = false;
            }
        }
        else {
            if (currTime >= 0) {
                currTime -= Time.deltaTime;
                moveLeft = true;
            }
            else {
                currTime += Time.deltaTime;
                moveLeft = false;
            }
        }

        currTime = Mathf.Clamp(currTime, minTime, maxTime);

        if (currTime > minTime && currTime < maxTime) {
            actualMoveSpeed = moveLeft ? -moveSpeed: moveSpeed;
        }
        foreach (var rope in ropeLeft) {
            rope.SetSize(-currTime * moveSpeed, -actualMoveSpeed);
        }
        foreach (var rope in ropeRight) {
            rope.SetSize(currTime * moveSpeed, actualMoveSpeed);
        }
    }
}