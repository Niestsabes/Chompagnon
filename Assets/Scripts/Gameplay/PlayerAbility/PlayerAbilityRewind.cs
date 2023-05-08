using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using UnityEngine.InputSystem;

/// <summary>
/// Ability to rewind the player's position several seconds back
/// </summary>
[RequireComponent(typeof(PositionRewinder))]
public class PlayerAbilityRewind : PlayerAbility
{
    [Header("Settings")]
    [SerializeField] protected float _maxRecordTime = 10;
    protected PositionRewinder _rewinder;
    public AudioClip Rewind;

    void Awake()
    {
        this._rewinder = this.GetComponent<PositionRewinder>();
        this._rewinder.maxRecordTime = this._maxRecordTime;
        this._rewinder.Record();
    }

    public override void OnAbility(InputAction.CallbackContext context)
    {
        if (GameManager.Instance.Atached)
        {
            GameManager.Instance.Detach();
        }
        if (context.started && !this._rewinder.isRewinding) StartCoroutine(this.Execute());
        else if (context.canceled && this._rewinder.isRewinding) this._rewinder.StopRewinding();
    }

    public override IEnumerator Execute()
    {
        GetComponent<PlayerController>().audioSource.PlayOneShot(Rewind);
        this.PlayForegroundEffect();
        this.InstantiateParticuleEffect();
        yield return this._rewinder.Rewind();
        this.InstantiateParticuleEffect();
        this.StopForegroundEffect();
        this._rewinder.Record();
    }

}
