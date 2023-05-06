using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

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

    public override IEnumerator Execute()
    {
        yield return this._rewinder.Rewind();
        this._rewinder.Record();
    }

}
