using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class TombObject : MonoBehaviour
    {
        [Header("Settings")]
        public Vector2 TombTpSpawnPoint;
        public Gradient colorGradient;
        public float topColorDist;
        public float bottomColorDist;
        public float triggerDist;
        public TombObject targetTomb;

        [Header("GameObject Components")]
        public SpriteRenderer _flameSprite;
        public ParticleSystem _particleSystem;

        protected PlayerAbilityTeleport playerController;

        void Awake()
        {
            this._particleSystem.Stop();
        }

        void Start()
        {
            this.playerController = GameManager.Instance.PlayersTransform[0].GetComponent<PlayerAbilityTeleport>();
        }

        void Update()
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
             if (this.IsTriggable(dist) && this.playerController.GetFocusedTomb() != this) this.SetAsTriggable(true);
            else if (this.playerController.GetFocusedTomb() == this && !this.IsTriggable(dist)) this.SetAsTriggable(false);
        }

        private float ComputeDistance()
        {
            if(this.playerController != null)
            {
                return (this.playerController.transform.position - this.transform.position).magnitude;
            }
            return 0;
        }

        private Color ComputeTombColor(float dist)
        {
            float rate = (dist - this.bottomColorDist) / (this.topColorDist - this.bottomColorDist);
            rate = rate > 1 ? 1 : (rate < 0 ? 0 : rate);
            return this.colorGradient.Evaluate(1 - rate);
        }

        private void ColorTomb(Color color)
        {
            this._flameSprite.color = color;
        }

        private bool IsTriggable(float dist)
        {
            return dist <= this.triggerDist;
        }

        private void SetAsTriggable(bool isTriggable)
        {
            if (isTriggable) {
                this.playerController.SetFocusedTomb(this);
                this._particleSystem.Play();
            } else {
                this.playerController.SetFocusedTomb(null);
                this._particleSystem.Stop();
            }
        }
    }
}