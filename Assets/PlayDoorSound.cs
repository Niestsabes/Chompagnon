using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDoorSound : MonoBehaviour
{
    public AudioClip Door;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlaySound() {
            if (GetComponent<AudioSource>().isPlaying) return;
            GetComponent<AudioSource>().PlayOneShot(Door);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
