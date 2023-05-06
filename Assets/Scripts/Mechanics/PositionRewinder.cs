using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class PositionRewinder : MonoBehaviour
    {
        private readonly float RECORD_DELAY = 0.25f;
        private readonly float MAX_RECORD_TIME = 10;
        private readonly float REWIND_SPEED_RATIO = 1f;
        private List<RewindStamp> listStamp = new List<RewindStamp>();
        private bool isRecording = false;
        private float internTime = 0;
        private Rigidbody2D _rigidbody;

        void Awake()
        {
            this._rigidbody = this.GetComponent<Rigidbody2D>();
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
            if (this._rigidbody) this._rigidbody.simulated = false;
            this.listStamp.Add(new RewindStamp() { position = this.transform.position, timestamp = this.internTime });
            for (int idx = this.listStamp.Count - 1; idx > 0; idx--) {
                yield return this.RunRewind(this.listStamp[idx], this.listStamp[idx - 1]);
            }
            if (this._rigidbody) this._rigidbody.simulated = true;
        }

        private IEnumerator RunRecording()
        {
            this.isRecording = true;
            this.listStamp = new List<RewindStamp>();
            this.internTime = 0;
            float recordTime = this.internTime + this.RECORD_DELAY;
            do {
                this.internTime += Time.deltaTime;

                // Stack new records
                if (this.internTime > recordTime) {
                    this.listStamp.Add(new RewindStamp() { position = this.transform.position, timestamp = this.internTime });
                    recordTime += this.RECORD_DELAY;
                }

                // Unstack old records
                if (this.listStamp.Count > 0) {
                    var oldestRecord = this.listStamp[0];
                    if (oldestRecord.timestamp < this.internTime - this.MAX_RECORD_TIME) this.listStamp.RemoveAt(0);
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
                rewindTime += Time.deltaTime * this.REWIND_SPEED_RATIO;
                this.transform.position = Vector3.Lerp(newestStamp.position, oldestStamp.position, rewindTime / endRewindTime);
                yield return null;
            }
            this.transform.position = oldestStamp.position;
        }
    }

    class RewindStamp
    {
        public Vector3 position { get; set; }
        public float timestamp { get; set; }
    }
}