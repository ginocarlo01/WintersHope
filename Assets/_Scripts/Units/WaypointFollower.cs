using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointFollower : MonoBehaviour
{
    [SerializeField]
    Transform[] path;

    [SerializeField]
    int currentPoint;

    [SerializeField]
    Transform currentGoal;

    [SerializeField]
    float moveSpeed;

    [SerializeField]
    float roundingDistance;

    Rigidbody2D rb;

    [SerializeField]
    Animator animator;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentGoal = path[currentPoint];

        EnemyController enemyController = GetComponent<EnemyController>();

        if(enemyController != null)
        {
            enemyController.DisableControlAnimation();
        }

        
    }

    void FixedUpdate()
    {
        Vector3 temp = Vector3.MoveTowards(transform.position, currentGoal.position, moveSpeed * Time.deltaTime);
        rb.MovePosition(temp);

        if(moveSpeed != 0)
        {
            animator.SetBool("moving", true);
        }

        if (Vector3.Distance(transform.position, currentGoal.position) < roundingDistance)
        {
            ChangeGoal();
        }
    }

    private void ChangeGoal()
    {
        if (currentPoint == path.Length - 1)
        {
            currentPoint = 0;
            currentGoal = path[currentPoint];
            UpdateAnimation();
        }
        else
        {
            currentPoint++;
            currentGoal = path[currentPoint];
            UpdateAnimation();
        }
    }

    void UpdateAnimation()
    {
        Vector2 direction = (currentGoal.position - transform.position).normalized;

        animator.SetFloat("moveX", direction.x);
        animator.SetFloat("moveY", direction.y);
    }

    private void OnEnable()
    {
        animator.SetBool("moving", true);
        UpdateAnimation();
    }

}
