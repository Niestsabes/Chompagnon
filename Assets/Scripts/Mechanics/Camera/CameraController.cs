using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Platformer.Mechanics
{
    public class CameraController : MonoBehaviour
    {
        [Header("Settigns")]
        public CinemachineVirtualCamera defaultVirtualCamera;

        public static CameraController instance { get { return CameraController._instance; } }
        private static CameraController _instance;

        private CinemachineBrain _brain;
        private CinemachineVirtualCamera[] _listVirtualCamera = new CinemachineVirtualCamera[0];

        void Awake()
        {
            if (CameraController._instance) Destroy(CameraController._instance);
            CameraController._instance = this;
            this._brain = this.GetComponent<CinemachineBrain>();
            this._listVirtualCamera = GameObject.FindObjectsOfType<CinemachineVirtualCamera>();
        }

        public void SwitchCamera(CinemachineVirtualCamera virtualCamera)
        {
            foreach (var vCam in this._listVirtualCamera)
            {
                if (vCam != virtualCamera) vCam.enabled = false;
                if (vCam == virtualCamera) vCam.enabled = true;
            }
        }

        public void SwitchToDefaultCamera()
        {
            this.SwitchCamera(this.defaultVirtualCamera);
        }
    }
}