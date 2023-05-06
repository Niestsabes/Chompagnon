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
        public float triggerDist;
        public Tomb targetTomb;

        [Header("GameObject Components")]
        public SpriteRenderer spriteRenderer;

        protected PlayerAbilityTeleport playerController;

        private void Awake()
        {
            this.playerController = GameObject.FindFirstObjectByType<PlayerAbilityTeleport>();
        }

        private void Update()
        {
            if (this.playerController != null) this.RunChecks();
        }

        /// <summary>
        /// Colors this tomb using its distance to the player
        /// Sets if triggable or not
        /// </summary>
        public void RunChecks()
        {
            float dist = this.ComputeDistance();
            float colorDist = dist;
            if (this.targetTomb != null) colorDist = Mathf.Min(dist, this.targetTomb.ComputeDistance());
            this.ColorTomb(this.ComputeTombColor(colorDist));
            if (this.IsTriggable(dist)) this.playerController.SetFocusedTomb(this);
            else if (this.playerController.GetFocusedTomb() == this && !this.IsTriggable(dist)) this.playerController.SetFocusedTomb(null);
        }

        private float ComputeDistance()
        {
            return (this.playerController.transform.position - this.transform.position).magnitude;
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

        private bool IsTriggable(float dist)
        {
            return dist <= this.triggerDist;
        }
    }
}