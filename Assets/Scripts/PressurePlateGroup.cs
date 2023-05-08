using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PressurePlateGroup : MonoBehaviour
{
    private PressurePlate[] pressurePlate;
    private int pushedPlates = 0;
    public UnityEvent onAllPushed;
    private bool stayPushed = true;
    // Start is called before the first frame update
    void Start()
    {
        pressurePlate = GetComponentsInChildren<PressurePlate>();
        foreach (PressurePlate pressurePlate in pressurePlate) {
            pressurePlate.eventTriggerEnter.AddListener(PushOne);
            pressurePlate.eventTriggerExit.AddListener(UnpushOne);
        }
    }

    public void PushOne() {
        pushedPlates++;

        if (pushedPlates == pressurePlate.Length) {
            if (stayPushed) {
                foreach (PressurePlate pressurePlate in pressurePlate) {
                    pressurePlate.StayPushed();
                }
            }
            onAllPushed.Invoke();
        }
    }

    public void UnpushOne() {
        pushedPlates--; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
