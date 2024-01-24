using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObstacle : MonoBehaviour
{
    [SerializeField]
    private int hitsToDestroy = 2;
    [SerializeField]
    private int hitsTaken;

    [SerializeField]
    private string projectileTag = "Enemy";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(projectileTag))
        {
            hitsTaken++;
            #region DisableProjectile
            IPooledObject objectFromPool = collision.GetComponent<IPooledObject>();

            if (objectFromPool != null)
            {
                objectFromPool.OnObjectDisabled();
            }
            #endregion
            CheckNumberOfHits();
        }
    }

    private void CheckNumberOfHits()
    {
        if(hitsTaken >= hitsToDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}
