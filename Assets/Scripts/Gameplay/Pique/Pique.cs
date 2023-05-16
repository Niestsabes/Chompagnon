using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pique : MonoBehaviour
{
    public bool killBoth = true;
    public bool once = false;
    public bool killed = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Contains("Player") && isActiveAndEnabled && !(killed && once))
        {
            if(GameManager.Instance.Atached && killBoth)
                GameManager.Instance.PlayersTransform[1 - GameManager.Instance._currentSquirrel].gameObject.GetComponent<PlayerDeath>().PlayerDeathAnim();
            collision.collider.GetComponent<PlayerDeath>().PlayerDeathAnimAndSound();
            killed = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Player") && isActiveAndEnabled && !(killed && once))
        {
            if(GameManager.Instance.Atached && killBoth)
                GameManager.Instance.PlayersTransform[1 - GameManager.Instance._currentSquirrel].gameObject.GetComponent<PlayerDeath>().PlayerDeathAnim();
            collision.GetComponent<PlayerDeath>().PlayerDeathAnimAndSound();
            killed = true;
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name.Contains("Player") && isActiveAndEnabled && !(killed && once))
        {
            if(GameManager.Instance.Atached && killBoth)
                GameManager.Instance.PlayersTransform[1 - GameManager.Instance._currentSquirrel].gameObject.GetComponent<PlayerDeath>().PlayerDeathAnim();
            collision.GetComponent<PlayerDeath>().PlayerDeathAnimAndSound();
            killed = true;
        }
    }
}
