using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using UnityEngine.InputSystem;

/// <summary>
/// Ability that teleports player to an associated tomb
/// </summary>
public class PlayerAbilityTeleport : PlayerAbility
{
    private TombObject focusedTomb;

    public override void OnAbility(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        StartCoroutine(this.Execute());
    }

    /// <summary>
    /// Teleport player to a distant tomb
    /// </summary>
    public override IEnumerator Execute()
    {
        if (this.focusedTomb && this.focusedTomb.targetTomb) {
            if (GameManager.Instance.Atached)
            {
                GameManager.Instance.Detach();
            }
            this.PlayForegroundEffect();
            this.InstantiateParticuleEffect();
            this.transform.position = this.focusedTomb.targetTomb.transform.position;
            this.InstantiateParticuleEffect();
            yield return new WaitForSeconds(0.4f);
            this.StopForegroundEffect();
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
