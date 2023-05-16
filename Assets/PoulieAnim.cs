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
    public float leftMultiplier = 1;
    public float rightMultiplier = 1;

    public void SetActivateLeft(bool activate) {

        activateLeft += activate ? 1 : -1;


        if (activateLeft != activateRight && currTime > minTime && currTime < maxTime) {
            if (GetComponent<AudioSource>().isPlaying) return;
            GetComponent<AudioSource>().PlayOneShot(Ascenseur);
        }
    }
    public void SetActivateRight(bool activate) {

        activateRight += activate ? 1 : -1;
        foreach (var wa in wheels) {
            wa.rotateRight = activateRight;
        }

        if (activateLeft != activateRight && currTime > minTime && currTime < maxTime) {
            if (GetComponent<AudioSource>().isPlaying) return;
            GetComponent<AudioSource>().PlayOneShot(Ascenseur);
        }
    }


    public void FixedUpdate() {
        float actualMoveSpeed = 0;
        bool moveLeft = false;
        bool moveRight = false;
        bool blocked = true;
        if (activateLeft != activateRight) {
            if (activateLeft > activateRight) {
                currTime -= Time.deltaTime;
                moveLeft = true;
            }
            else {
                currTime += Time.deltaTime;
                moveRight = true;
            }
        }
        else {
            if (currTime >= 0.1f) {
                currTime -= Time.deltaTime;
                moveLeft = true;
            }
            else if (currTime <= -0.1f){
                currTime += Time.deltaTime;
                moveRight = true;
            }
        }

        currTime = Mathf.Clamp(currTime, minTime, maxTime);


        if (currTime > minTime && currTime < maxTime && (moveLeft || moveRight)) {
            blocked = false;
            actualMoveSpeed = moveLeft ? -moveSpeed: moveSpeed;
        }
        foreach (var rope in ropeLeft) {
            rope.SetSize(-currTime * moveSpeed * leftMultiplier, -actualMoveSpeed * leftMultiplier);
        }
        foreach (var rope in ropeRight) {
            rope.SetSize(currTime * moveSpeed * rightMultiplier, actualMoveSpeed * rightMultiplier);
        }
        foreach (var wa in wheels) {
            wa.blocked = blocked;
            wa.rotateLeft = moveLeft ? 1 : 0;
            wa.rotateRight = moveRight ? 1 : 0;
        }
    }
}