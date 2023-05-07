using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pique : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.name.Contains("Player"))
        {
            GameManager.Instance.Death();
        }
    }
}
