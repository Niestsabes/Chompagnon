using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public abstract class PlayerAbility : MonoBehaviour
{
    public void OnAbility(InputAction.CallbackContext context)
    {
        if (!context.started) return;
        StartCoroutine(this.Execute());
    }

    public abstract IEnumerator Execute();
}
