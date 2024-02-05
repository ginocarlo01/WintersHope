using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject attackObject;

    private Animator attackObjectAnimator;

    [SerializeField]
    private CursorFollower cursorFollower;

    [SerializeField]
    private float attackAnimationTime = .2f;

    [SerializeField]
    private float attackMaxTime = 1f;

    [SerializeField]
    private float attackUpTime = .1f;

    [SerializeField]
    private float attackDownTime = .1f;

    [SerializeField]
    private float attackCoolDown = .6f;

    [SerializeField]
    private float attackUpCoolDown = .6f;

    private float attackTimer;

    private float coolDownTimer;

    private bool canAttack;

    private bool attacking;

    private bool cooling;

    void Start()
    {
        attackTimer = attackMaxTime;
        attackObjectAnimator = attackObject.GetComponent<Animator>();
    }

    void Update()
    {
        /*if(cooling)
        {
            coolDownTimer += Time.deltaTime * attackUpCoolDown;
            if(coolDownTimer >= attackCoolDown )
            {
                coolDownTimer = 0;
                cooling = false;
            }
        }


        if(attackTimer <= attackMaxTime && !attacking)
        {
            attackTimer += Time.deltaTime * attackUpTime;
        }

        if (attacking)
        {
            attackTimer -= Time.deltaTime * attackDownTime;
        }

        if (attackTimer > 0 && !cooling)
        {
            canAttack = true;
        }
        else
        {
            canAttack = false;
            cooling = true;
        }*/

        if (Input.GetMouseButtonDown(0) && !attacking)
        {

            Attack() ;
        }
        
    }

    private void Attack()
    {
        attacking = true;
        if (attackObject != null)
        {
            //attackObject.SetActive(true);
            attackObjectAnimator.SetTrigger("attack");
            cursorFollower.DisableFollow();
            StartCoroutine(WaitAttackAnimation());
        }
    }

    private void DisableAttack()
    {
        attacking = false;
        if (attackObject != null)
        {
            //attackObject.SetActive(false);
        }
    }

    private IEnumerator WaitAttackAnimation()
    {
        yield return new WaitForSeconds(attackAnimationTime);
        cursorFollower.EnableFollow();
        attacking = false;
    }
}
