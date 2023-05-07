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

    public abstract void OnAbility(InputAction.CallbackContext context);

    public abstract IEnumerator Execute();

    protected GameObject InstantiateParticuleEffect()
    {
        return GameObject.Instantiate(this.particleParfab, this.transform.position, this.transform.rotation);
    }
}
