using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObstacleByType : HitObstacle
{
    [SerializeField]
    private TypeUtility.Type type;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(projectileTag))
        {
            ProjectileController projectile = collision.GetComponent<ProjectileController>();

            if (projectile != null)
            {
                if (this.CanBeHit(projectile.GetProjectileType()))
                {
                    hitsTaken++;
                    CheckNumberOfHits();
                }
                DisableProjectile(collision);
            }
            
        }
    }

    public bool CanBeHit(TypeUtility.Type attackerType)
    {
        if (TypeUtility.HasAdvantage(attackerType, type))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
