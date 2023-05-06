using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;

public class PlayerAbilityTeleport : PlayerAbility
{
    public Tomb focusedTomb;

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

    public Tomb GetFocusedTomb()
    {
        return this.focusedTomb;
    }

    public void SetFocusedTomb(Tomb tomb)
    {
        if (tomb == null) { this.focusedTomb = null; return; }
        this.focusedTomb = tomb;
    }
}
