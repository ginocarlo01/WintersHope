using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    [SerializeField]
    private float damage = 2f;

    [SerializeField]
    private float destroyTime = 2f;

    [SerializeField]
    private string playerTag = "Player";

    void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }

    void Update()
    {
        transform.Translate(speed * new Vector3(0,1,0) * Time.deltaTime);
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(playerTag))
        {
            collision.gameObject.GetComponent<Life>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
    }
}
