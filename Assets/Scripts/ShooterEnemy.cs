using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemy : MonoBehaviour
{
    [SerializeField]
    private Transform[] shootPoints;

    [SerializeField]
    private float attackCoolDown;

    private float attackTimer;

    [SerializeField]
    private float attackUpCoolDown = .6f;

    [SerializeField]
    private GameObject projectile;

    void Start()
    {
        
    }

    void Update()
    {
        attackTimer += Time.deltaTime * attackUpCoolDown;

        if(attackTimer > attackCoolDown)
        {
            attackTimer = 0;
            Attack();
        }
    }

    private void Attack()
    {
        for(int i = 0; i < shootPoints.Length; i++)
        {
            Instantiate(projectile, shootPoints[i].position, Quaternion.identity);
        }
    }


}
