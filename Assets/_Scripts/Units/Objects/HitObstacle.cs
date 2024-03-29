using System;
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

    public static Action<SFX> hitActionSFX;
    [SerializeField] protected SFX hitSFX;

    [SerializeField]
    protected Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetTrigger("Basic");

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(projectileTag))
        {
            hitsTaken++;
            hitActionSFX?.Invoke(hitSFX);
            DisableProjectile(collision);
            animator.SetTrigger("Hit");
            //CheckNumberOfHits();
        }
    }

    protected void CheckNumberOfHits()
    {
        if(hitsTaken >= hitsToDestroy)
        {
            this.gameObject.SetActive(false);   
        }
    }

    public void DisableObstacle()
    {
        this.gameObject.SetActive(false);
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
