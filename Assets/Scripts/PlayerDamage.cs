using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField]
    private string enemyTag = "Enemy";

    [SerializeField]
    private float damage = 2f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag(enemyTag))
        {
            collision.gameObject.GetComponent<Life>().TakeDamage(damage);
        }
    }
}
