using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPillar : MonoBehaviour
{
    public AudioClip _fallingPillarSound;
    public void SoundFallingPillar()
    {
        if (GetComponent<AudioSource>().isPlaying) return;
        GetComponent<AudioSource>().PlayOneShot(_fallingPillarSound);
    }
}
