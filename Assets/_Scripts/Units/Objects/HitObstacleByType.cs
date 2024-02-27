using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitObstacleByType : HitObstacle, IHit
{
    [SerializeField]
    private TypeUtility.Type type;

    

    public event Action OnHit;


    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger(type.ToString());
    }

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
                    animator.SetTrigger("Hit");
                    OnHit?.Invoke();
                    hitActionSFX?.Invoke(hitSFX);
                    //CheckNumberOfHits();
                }
                DisableProjectile(collision);
            }
            
        }
    }

    public bool CanBeHit(TypeUtility.Type attackerType)
    {
        if (attackerType == type)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
