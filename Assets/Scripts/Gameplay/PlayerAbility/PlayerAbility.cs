using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Platformer.Mechanics;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerController))]
public abstract class PlayerAbility : MonoBehaviour
{
    public void OnFire(InputValue value)
    {
        Debug.Log(value.Get<float>());
        if (value.Get<float>() > 0) StartCoroutine(this.Execute());
    }

    public abstract IEnumerator Execute();
}
