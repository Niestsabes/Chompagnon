using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Transform SpawnPoint;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name.Contains("Player"))
        {
            SpawnPoint.position = new Vector2(gameObject.transform.position.x, 0);
        }
    }
}
