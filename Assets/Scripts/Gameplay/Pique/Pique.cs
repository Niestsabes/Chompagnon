using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pique : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Contains("Player") && isActiveAndEnabled)
        {
            if(GameManager.Instance.Atached)
                GameManager.Instance.PlayersTransform[1 - GameManager.Instance._currentSquirrel].gameObject.GetComponent<PlayerDeath>().PlayerDeathAnim();
            collision.collider.GetComponent<PlayerDeath>().PlayerDeathAnimAndSound();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Player") && isActiveAndEnabled)
        {
            if(GameManager.Instance.Atached)
                GameManager.Instance.PlayersTransform[1 - GameManager.Instance._currentSquirrel].gameObject.GetComponent<PlayerDeath>().PlayerDeathAnim();
            collision.GetComponent<PlayerDeath>().PlayerDeathAnimAndSound();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Contains("Player") && isActiveAndEnabled)
        {
            if(GameManager.Instance.Atached)
                GameManager.Instance.PlayersTransform[1 - GameManager.Instance._currentSquirrel].gameObject.GetComponent<PlayerDeath>().PlayerDeathAnim();
            collision.GetComponent<PlayerDeath>().PlayerDeathAnimAndSound();
        }
    }
}
