using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCollider : MonoBehaviour
{
    [SerializeField]
    string enemyTag;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(enemyTag))
        {

            #region DisableProjectile
            IPooledObject objectFromPool = collision.GetComponent<IPooledObject>();

            if (objectFromPool != null)
            {
                objectFromPool.OnObjectDisabled();
            }
            #endregion
        }
    }
}
