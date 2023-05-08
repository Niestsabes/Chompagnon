using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public abstract class PlayerAbility : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject particleParfab;

    [Header("Setting")]
    [SerializeField] public Color _backgroundColor;
    [SerializeField] public Color _foregroundColor;

    public abstract void OnAbility(InputAction.CallbackContext context);

    public abstract IEnumerator Execute();

    protected GameObject InstantiateParticuleEffect()
    {
        return GameObject.Instantiate(this.particleParfab, this.transform.position, this.transform.rotation);
    }

    protected void PlayForegroundEffect()
    {
        if (UIAbilityForeground.instance == null) return;
        UIAbilityForeground.instance.SetBackground(this._backgroundColor);
        UIAbilityForeground.instance.SetForeground(this._foregroundColor);
        UIAbilityForeground.instance.Play();
    }

    protected void StopForegroundEffect()
    {
        if (UIAbilityForeground.instance == null) return;
        UIAbilityForeground.instance.Stop();
    }
}
