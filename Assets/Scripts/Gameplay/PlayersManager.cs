using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class PlayersManager : MonoBehaviour
{
    public void MovePlayers(InputAction.CallbackContext context)
    {
        gameObject.GetComponentInChildren<PlayerController>().OnMove(context);
    }

    public void Switch(InputAction.CallbackContext context)
    {
        gameObject.GetComponentInChildren<PlayerController>().Switch(context);
    }

    public void Detach(InputAction.CallbackContext context)
    {
        gameObject.GetComponentInChildren<PlayerController>().Detach(context);
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        gameObject.GetComponentInChildren<PlayerController>().OnJump(context);
    }

    public void Ability(InputAction.CallbackContext context)
    {
        if (!gameObject.GetComponentInChildren<PlayerController>().controlEnabled) return;
        gameObject.GetComponentInChildren<PlayerAbility>().OnAbility(context);
    }

    public void Pause(InputAction.CallbackContext context)
    {
        Debug.Log("Hello");
        //if (!gameObject.GetComponentInChildren<PlayerController>().controlEnabled) return;
        //gameObject.GetComponentInChildren<PlayerAbility>().OnAbility(context);
    }
}
