using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TombThunderObject), typeof(Pique))]
public class OneTimeThunder : MonoBehaviour
{
    public float thunderOffset = 0.5f;
    public float activateOffset = 1.0f;
    public float deactivateOffset = 2.0f;
    // Start is called before the first frame update
    public void Hit()
    {
        Invoke("LaunchThunder", thunderOffset);
    }

    public void LaunchThunder() {
        GetComponent<TombThunderObject>().Hit();
        Invoke("ActivatePique", activateOffset);
        Invoke("DeactivatePique", deactivateOffset);
    }
    void ActivatePique() {
        GetComponent<Pique>().enabled = true;
    }
    void DeactivatePique() {
        GetComponent<Pique>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
