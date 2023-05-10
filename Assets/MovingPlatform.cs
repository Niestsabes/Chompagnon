using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Rigidbody2D body;

    // Start is called before the first frame update
    void Start()
    {

        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (other.TryGetComponent<PlayerController>(out PlayerController controller)) {
                if (controller.IsGrounded && !controller.onPlatform) {
                    controller.onPlatform = true;
                    controller.platformVelocity = body.velocity;
                }
                else {
                    controller.onPlatform = false;
                    controller.platformVelocity = new Vector2(0f, 0f);
                }
            }
        } 
    }
    private void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (other.TryGetComponent<PlayerController>(out PlayerController controller)) {
                if (controller.IsGrounded) {
                    controller.onPlatform = true;
                    controller.platformVelocity = body.velocity;
                }
                else {
                    controller.onPlatform = false;
                    controller.platformVelocity = new Vector2(0f, 0f);
                }
            }
        } 
    }
    private void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Player")) {
            if (other.TryGetComponent<PlayerController>(out PlayerController controller)) {
                 controller.onPlatform = false;
                 controller.platformVelocity = new Vector2(0f, 0f);
            }
        } 
    }
}
