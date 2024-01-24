using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObstacle : MonoBehaviour
{
    [SerializeField]
    protected int hitsToDestroy = 2;
    [SerializeField]
    protected int hitsTaken;

    [SerializeField]
    protected string projectileTag = "Enemy";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(projectileTag))
        {
            hitsTaken++; 
            DisableProjectile(collision);
            CheckNumberOfHits();
        }
    }

    protected void CheckNumberOfHits()
    {
        if(hitsTaken >= hitsToDestroy)
        {
            this.gameObject.SetActive(false);   
        }
    }

    protected void DisableProjectile(Collider2D collision)
    {
        IPooledObject objectFromPool = collision.GetComponent<IPooledObject>();

        if (objectFromPool != null)
        {
            objectFromPool.OnObjectDisabled();
        }
    }
}
