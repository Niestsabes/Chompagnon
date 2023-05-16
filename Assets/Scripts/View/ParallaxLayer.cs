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

        public Transform maxTransform;
        public Transform minTransform;

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

            if (maxTransform != null) {
                Vector3 maxPosition = maxTransform.position - _initialCameraPosition + _initialPosition;
                target.x = Mathf.Min(target.x, maxPosition.x);
                target.y = Mathf.Min(target.y, maxPosition.y);
                target.z = Mathf.Min(target.z, maxPosition.z);
            }
            if (minTransform != null) {
                Vector3 minPosition = minTransform.position - _initialCameraPosition + _initialPosition;
                target.x = Mathf.Max(target.x, minPosition.x);
                target.y = Mathf.Max(target.y, minPosition.y);
                target.z = Mathf.Max(target.z, minPosition.z);
            }
            transform.position = target;

        }

    }
}