﻿using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    PlayerController playerController;
    public AudioClip Death;
    public bool dying = false;
    public void Start()
    {
       playerController = GetComponent<PlayerController>();
    }

    IEnumerator DelayDeath()
    {
        playerController.animator.SetTrigger("Dead");
        playerController.audioSource.PlayOneShot(Death);
        yield return null;
        yield return new WaitUntil(() => playerController.animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1f);
        
        if (playerController.jumpState == PlayerController.JumpState.Grounded)
        {
            playerController.controlEnabled = true;
            GameManager.Instance.Death(playerController.squirrelId);
        }
        dying = false;
        
    }

    IEnumerator DelayDeathForSecondChar()
    {
        playerController.animator.SetTrigger("Dead");
        yield return null;
    }

    public void PlayerDeathAnimAndSound()
    {
        if (!dying) {
            dying = true;
            playerController.controlEnabled = false;
            if (playerController.audioSource && playerController.ouchAudio)
                playerController.audioSource.PlayOneShot(playerController.ouchAudio);
            StartCoroutine(DelayDeath());
        }
    }

    public void PlayerDeathAnim()
    {
        StartCoroutine(DelayDeathForSecondChar());
    }
}