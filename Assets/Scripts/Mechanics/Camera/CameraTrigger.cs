using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Platformer.Mechanics
{
    [RequireComponent(typeof(Collider2D))]
    public class CameraTrigger : MonoBehaviour
    {
        [Header("Settings")]
        public CinemachineVirtualCamera virtualCamera;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player == null) { return; }
            this.virtualCamera.enabled = true;
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player == null) { return; }
            this.virtualCamera.enabled = false;
        }
    }
}