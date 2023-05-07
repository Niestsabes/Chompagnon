using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Gameplay;
using static Platformer.Core.Simulation;
using Platformer.Model;
using Platformer.Core;
using UnityEngine.InputSystem;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This is the main class used to implement control of the player.
    /// It is a superset of the AnimationController class, but is inlined to allow for any kind of customisation.
    /// </summary>
    public class PlayerController : KinematicObject
    {
        public int itSwitch = 0;
        public int itDetach = 0;
        public bool Second = false;
        public bool SecondPos = false;
        public AudioClip jumpAudio;
        public AudioClip respawnAudio;
        public AudioClip ouchAudio;

        /// <summary>
        /// Max horizontal speed of the player.
        /// </summary>
        public float maxSpeed = 7;
        /// <summary>
        /// Initial jump velocity at the start of a jump.
        /// </summary>
        public float jumpTakeOffSpeed = 7;
        public float offSet;
        public JumpState jumpState = JumpState.Grounded;
        private bool stopJump;
        /*internal new*/
        public Collider2D collider2d;
        /*internal new*/
        public AudioSource audioSource;
        public Health health;
        public bool controlEnabled = true;

        bool jump;
        public Vector2 move;
        SpriteRenderer spriteRenderer;
        internal Animator animator;
        readonly PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public Bounds Bounds => collider2d.bounds;

        void Awake()
        {
            health = GetComponent<Health>();
            audioSource = GetComponent<AudioSource>();
            collider2d = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
            animator = GetComponent<Animator>();
            transform.position = new Vector3(0 - offSet, 0, 0);
        }

        protected override void Update()
        {
            UpdateJumpState();
            base.Update();
        }

        public void Switch(InputAction.CallbackContext context)
        {
            if (!context.canceled) return;
            GameManager.Instance.SwitchSquirrel();

        }
        public void Detach(InputAction.CallbackContext context)
        {
            if (!context.canceled) return;
            GameManager.Instance.Detach();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            Transform[] gameObjects = gameObject.GetComponentsInChildren<Transform>();
            if (!context.performed ) 
            { 
                move.x = 0;
                
                foreach (Transform player in gameObjects)
                {
                    if (player.name != gameObject.name)
                        player.GetComponentInChildren<PlayerController>().MoveSecond(Vector2.zero);
                }
                return;
                
            }
            if (controlEnabled)
            {
                move.x = context.ReadValue<Vector2>().x;
                foreach (Transform player in gameObjects)
                {
                    if (player.name != gameObject.name)
                        player.GetComponent<PlayerController>().MoveSecond(context.ReadValue<Vector2>());
                }
                

            }
            else
            {
                move.x = 0;
                foreach (Transform player in gameObjects)
                {
                    if (player.name != gameObject.name)
                        player.GetComponentInChildren<PlayerController>().MoveSecond(Vector2.zero);
                }
            }

        }

        public void MoveSecond(Vector2 value)
        {
            move.x = value.x;
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            if (!context.performed)
            {
                stopJump = true;
                Schedule<PlayerStopJump>().player = this;
                return;
            }
            if (controlEnabled)
            {
                float isJump = context.ReadValue<float>();
                if (jumpState == JumpState.Grounded && isJump > 0)
                {
                    jumpState = JumpState.PrepareToJump;
                }
                else if (isJump <= 0)
                {
                    stopJump = true;
                    Schedule<PlayerStopJump>().player = this;
                }
            }
        }

        void UpdateJumpState()
        {
            jump = false;
            switch (jumpState)
            {
                case JumpState.PrepareToJump:
                    jumpState = JumpState.Jumping;
                    jump = true;
                    stopJump = false;
                    break;
                case JumpState.Jumping:
                    if (!IsGrounded)
                    {
                        Schedule<PlayerJumped>().player = this;
                        jumpState = JumpState.InFlight;
                    }
                    break;
                case JumpState.InFlight:
                    if (IsGrounded)
                    {
                        Schedule<PlayerLanded>().player = this;
                        jumpState = JumpState.Landed;
                    }
                    break;
                case JumpState.Landed:
                    jumpState = JumpState.Grounded;
                    break;
            }
        }

        protected override void ComputeVelocity()
        {
            if (jump && IsGrounded)
            {
                velocity.y = jumpTakeOffSpeed * model.jumpModifier;
                jump = false;
            }
            else if (stopJump)
            {
                stopJump = false;
                if (velocity.y > 0)
                {
                    velocity.y = velocity.y * model.jumpDeceleration;
                }
            }

            if (move.x > 0.01f)
                spriteRenderer.flipX = false;
            else if (move.x < -0.01f)
                spriteRenderer.flipX = true;

            animator.SetBool("grounded", IsGrounded);
            animator.SetFloat("velocityX", Mathf.Abs(velocity.x) / maxSpeed);

            targetVelocity = move * maxSpeed;
        }

        public enum JumpState
        {
            Grounded,
            PrepareToJump,
            Jumping,
            InFlight,
            Landed
        }
    }
}