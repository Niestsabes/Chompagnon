using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class audioTest : MonoBehaviour
{
    AudioSource m_MyAudioSource;
    public AudioSource m_stinger;
    public AudioSource m_Test;

    //Play the music
    bool m_Play;
    //Detect when you use the toggle, ensures music isn�t played multiple times
    bool m_ToggleChange;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Player"))
        {
            m_Play = false;
            m_ToggleChange = true;
            GetComponent<Collider2D>().enabled = false;
            StartCoroutine("TriggerStinger");
           
        }
    }

    IEnumerator TriggerStinger()
    {
        m_stinger.Play();
        yield return new WaitForSeconds(2);
        m_Test.Play();
        yield return null;
    }


    // Start is called before the first frame update
    void Start()
    {
        //Fetch the AudioSource from the GameObject
        m_MyAudioSource = GetComponent<AudioSource>();
        //Ensure the toggle is set to true for the music to play at start-up
        m_Play = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Check to see if you just set the toggle to positive
        if (m_Play == true && m_ToggleChange == true)
        {
            //Play the audio you attach to the AudioSource component
            m_MyAudioSource.Play();
            //Ensure audio doesn�t play more than once
            m_ToggleChange = false;
        }
        //Check if you just set the toggle to false
        if (m_Play == false && m_ToggleChange == true)
        {
            //Stop the audio
            m_MyAudioSource.Stop();
            //Ensure audio doesn�t play more than once
            m_ToggleChange = false;
        }
    }
}
