using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class Tomb : MonoBehaviour
    {
        [Header("Settings")]
        public Gradient colorGradient;
        public float topColorDist;
        public float bottomColorDist;
        public Tomb targetTomb;

        [Header("GameObject Components")]
        public SpriteRenderer spriteRenderer;

        protected PlayerController[] listPlayerController;

        private void Awake()
        {
            this.listPlayerController = GameObject.FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
        }

        private void Update()
        {
            float dist = this.ComputeDistance();
            if (this.targetTomb != null) dist = Mathf.Min(dist, this.targetTomb.ComputeDistance());
            Color color = this.ComputeTombColor(dist);
            this.ColorTomb(color);
        }

        private float ComputeDistance()
        {
            float minDist = Mathf.Infinity;
            foreach (var player in this.listPlayerController) {
                float dist = (player.transform.position - this.transform.position).magnitude;
                if (dist < minDist) minDist = dist;
            }
            return minDist;
        }

        private Color ComputeTombColor(float dist)
        {
            float rate = (dist - this.bottomColorDist) / (this.topColorDist - this.bottomColorDist);
            rate = rate > 1 ? 1 : (rate < 0 ? 0 : rate);
            return this.colorGradient.Evaluate(1 - rate);
        }

        private void ColorTomb(Color color)
        {
            this.spriteRenderer.color = color;
        }
    }
}