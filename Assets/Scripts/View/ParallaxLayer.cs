using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.View
{
    /// <summary>
    /// Used to move a transform relative to the main camera position with a scale factor applied.
    /// This is used to implement parallax scrolling effects on different branches of gameobjects.
    /// </summary>
    public class ParallaxLayer : MonoBehaviour
    {
        public Vector3 movementScale = Vector3.one;
        public Vector3 offset = Vector3.zero;
        private Transform _camera;

        void Awake()
        {
            this._camera = Camera.main.transform;
        }

        void Update()
        {
            var target = Vector3.Scale(_camera.position, movementScale) + offset;
            transform.position = target;
        }

    }
}