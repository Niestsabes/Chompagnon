using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlate : MonoBehaviour
{

    public UnityEvent eventTriggerEnter;
    public UnityEvent eventTriggerExit;
    public UnityEvent eventTriggerStayPushed;
    public string filterTag = "Player";
    public bool shouldBeGrounded = false;
    private bool stayPushed = false;
    private int pushed = 0;

    public void StayPushed() {
        stayPushed = true;
        if (pushed == 0)
            eventTriggerEnter.Invoke();
        eventTriggerStayPushed.Invoke();
    }
    private void OnTriggerEnter2D(Collider2D collision) {
        if (stayPushed)
            return;
        if (collision.gameObject.CompareTag(filterTag)) {
            if (collision.gameObject.TryGetComponent<PlayerController>(out PlayerController playerController)) {
                if (playerController.IsGrounded || !shouldBeGrounded) {
                    if (pushed == 0)
                        eventTriggerEnter.Invoke();
                    pushed++;
                }
            }
            else {
                if (pushed == 0)
                    eventTriggerEnter.Invoke();
                pushed++;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision) {
        if (stayPushed)
            return;
        if (collision.gameObject.CompareTag(filterTag)) {
            if (pushed == 1)
                eventTriggerExit.Invoke();
            pushed--;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
