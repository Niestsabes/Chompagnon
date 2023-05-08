using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

[RequireComponent(typeof(Animator))]
public class TombThunderObject : MonoBehaviour
{
    [Header("Settigns")]
    [SerializeField] private bool autoTrigger = true;
    [SerializeField] private float _minReloadTime = 5;
    [SerializeField] private float _distThreshold = 1;
    [SerializeField] private ParticleSystem _particleSystem;

    private Animator _animator;
    private AudioSource _audioSource;
    private float _cooldown;
    private PlayerController[] _listPlayer;

    void Awake()
    {
        this._animator = this.GetComponent<Animator>();
        this._audioSource = this.GetComponent<AudioSource>();
        this._listPlayer = GameObject.FindObjectsByType<PlayerController>(FindObjectsSortMode.None);
    }

    void Update()
    {
        if (this.autoTrigger) this.ReduceCooldown();
    }

    protected void ReduceCooldown()
    {
        foreach (var player in this._listPlayer) {
            if ((player.transform.position - this.transform.position).magnitude < this._distThreshold) {
                this._cooldown -= Time.deltaTime;
                if (this._cooldown <= 0) { this.Hit(); }
                break;
            }
        }
    }

    public void Hit()
    {
        this._animator.SetTrigger("trigger");
        this._audioSource.Play();
        this._cooldown = this._minReloadTime * (1 + Random.value * 0.5f);
    }

    public void Explode()
    {
        this._particleSystem.Play();
    }
}
