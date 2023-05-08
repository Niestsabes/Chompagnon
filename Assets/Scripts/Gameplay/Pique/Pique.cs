using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pique : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Contains("Player"))
        {
            if(GameManager.Instance.Atached)
                GameManager.Instance.PlayersTransform[1 - GameManager.Instance._currentSquirrel].gameObject.GetComponent<PlayerDeath>().PlayerDeathAnim();
            collision.collider.GetComponent<PlayerDeath>().PlayerDeathAnimAndSound();
        }
    }
}
