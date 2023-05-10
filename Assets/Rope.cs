using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public bool down = false;
    private float initialSize;
    public Transform[] attachedObjects;
    private Vector3[] attachedObjectsInitial;

    private void Start() {
        initialSize = transform.localScale.y;

        attachedObjectsInitial = new Vector3[attachedObjects.Length];
        for (int i = 0; i < attachedObjects.Length; i++) {
            attachedObjectsInitial[i] = attachedObjects[i].position;
        }
    }
    public void SetSize(float metersOffset, float speed) {
        transform.localScale = new Vector3(.1f, metersOffset + initialSize, 1);

        for (int i = 0; i < attachedObjects.Length; i++) {

            if (attachedObjects[i].TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody2D)) {
                rigidbody2D.velocity = new Vector2(0, speed * (down ? -1 : 1));
            }
            else {
                attachedObjects[i].position = attachedObjectsInitial[i] + new Vector3(0, metersOffset * (down ? -1: 1), 0);
            }
        }
        
    }
}
