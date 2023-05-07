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
    protected PositionRewinder _rewinder;

    void Awake()
    {
        this._rewinder = this.GetComponent<PositionRewinder>();
        if (this._rewinder) this._rewinder.Record();
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
        this.InstantiateParticuleEffect();
        yield return this._rewinder.Rewind();
        this.InstantiateParticuleEffect();
        this._rewinder.Record();
    }

}
