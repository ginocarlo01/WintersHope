using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeBorder : MonoBehaviour
{
    [SerializeField]
    private string enemyTag = "Enemy";

    [SerializeField]
    private int life;



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enemyTag))
        {
            
            Destroy(collision.gameObject);
        }
    }
}
