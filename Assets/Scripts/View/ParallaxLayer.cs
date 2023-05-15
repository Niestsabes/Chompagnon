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
        private Transform _camera;

        public Transform _referenceCameraTransform;
        private Vector3 _initialCameraPosition;
        private Vector3 _initialPosition;

        void Awake()
        {
            this._camera = Camera.main.transform;
            this._initialPosition = transform.position;
            this._initialCameraPosition = transform.position;
        }

        void LateUpdate()
        {
            if (_referenceCameraTransform != null) {
                _initialCameraPosition = _referenceCameraTransform.position;
            }
            var target = Vector3.Scale(_camera.position - _initialCameraPosition, movementScale) + _initialPosition;
            transform.position = target;
        }

    }
}