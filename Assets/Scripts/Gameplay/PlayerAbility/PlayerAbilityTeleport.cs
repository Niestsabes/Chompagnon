using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

/// <summary>
/// Ability that teleports player to an associated tomb
/// </summary>
public class PlayerAbilityTeleport : PlayerAbility
{
    private TombObject focusedTomb;

    /// <summary>
    /// Teleport player to a distant tomb
    /// </summary>
    public override IEnumerator Execute()
    {
        if (this.focusedTomb && this.focusedTomb.targetTomb) {
            this.transform.position = this.focusedTomb.targetTomb.transform.position;
        }
        yield return null;
    }

    public TombObject GetFocusedTomb()
    {
        return this.focusedTomb;
    }

    public void SetFocusedTomb(TombObject tomb)
    {
        if (tomb == null) { this.focusedTomb = null; return; }
        this.focusedTomb = tomb;
    }
}
