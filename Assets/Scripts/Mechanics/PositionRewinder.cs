using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class PositionRewinder : MonoBehaviour
    {
        public float RECORD_DELAY = 0.25f;
        public float REWIND_SPEED_RATIO = 2f;
        private List<RewindStamp> listStamp = new List<RewindStamp>();
        public bool isRecording { get; protected set; } = false;
        public bool isRewinding { get; protected set; } = false;
        public float maxRecordTime { get; set; } = 10;
        private float internTime = 0;
        private Rigidbody2D _rigidbody;
        private Collider2D _collider;
        private PlayerController _playerController;

        void Awake()
        {
            this._rigidbody = this.GetComponent<Rigidbody2D>();
            this._collider = this.GetComponent<Collider2D>();
            this._playerController = this.GetComponent<PlayerController>();
        }

        public void Record()
        {
            if (this.isRecording) { return; }
            StartCoroutine(this.RunRecording());
        }

        public void StopRecording()
        {
            this.isRecording = false;
        }

        public IEnumerator Rewind()
        {
            this.StopRecording();
            this.isRewinding = true;
            this.SetObjectInteractionEnabled(false);
            this.listStamp.Add(new RewindStamp() { position = this.transform.position, timestamp = this.internTime });
            for (int idx = this.listStamp.Count - 1; idx > 0; idx--) {
                if (!this.isRewinding) break;
                yield return this.RunRewind(this.listStamp[idx], this.listStamp[idx - 1]);
            }
            this.isRewinding = false;
            this.SetObjectInteractionEnabled(true);
        }

        public void StopRewinding()
        {
            this.isRewinding = false;
        }

        private IEnumerator RunRecording()
        {
            this.isRecording = true;
            this.internTime = 0;
            float recordTime = this.internTime + this.RECORD_DELAY;
            do {

                if ((_playerController.velocity + _playerController.platformVelocity).magnitude >= 0.001)
                    this.internTime += Time.deltaTime;

                // Stack new records
                if (this.internTime > recordTime) {
                    this.listStamp.Add(new RewindStamp() { position = this.transform.position, timestamp = this.internTime });
                    recordTime += this.RECORD_DELAY;
                }

                // Unstack old records
                if (this.listStamp.Count > 0) {
                    var oldestRecord = this.listStamp[0];
                    if (oldestRecord.timestamp < this.internTime - this.maxRecordTime) this.listStamp.RemoveAt(0);
                }

                // Wait new frame
                yield return new WaitForFixedUpdate();
            } while (this.isRecording);
        }

        private IEnumerator RunRewind(RewindStamp newestStamp, RewindStamp oldestStamp)
        {
            float rewindTime = 0;
            float endRewindTime = newestStamp.timestamp - oldestStamp.timestamp;
            while (rewindTime < endRewindTime) {
                if (!this.isRewinding) break;
                rewindTime += Time.deltaTime * this.REWIND_SPEED_RATIO;
                this.transform.position = Vector3.Lerp(newestStamp.position, oldestStamp.position, rewindTime / endRewindTime);
                yield return null;
            }
            if (this.isRewinding) this.transform.position = oldestStamp.position;
        }

        private void SetObjectInteractionEnabled(bool isEnabled)
        {
            gameObject.GetComponent<PlayerController>().isRewinding = isEnabled;
            if (this._rigidbody) this._rigidbody.simulated = isEnabled;
            if (this._collider) this._collider.enabled = isEnabled;
        }
    }

    class RewindStamp
    {
        public Vector3 position { get; set; }
        public float timestamp { get; set; }
    }
}
