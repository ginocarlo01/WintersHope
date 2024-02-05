using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddHealthToPlayer : MonoBehaviour
{
    [SerializeField]
    private string playerTag = "Player";

    [SerializeField]
    private int upHealth = 1;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == playerTag)
        {
            collision.GetComponent<LifeBorder>().TakeDamage(-upHealth);
            gameObject.SetActive(false);
        }
    }
}
