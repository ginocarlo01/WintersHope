using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D rb;

    Vector3 changeMovementInput;

    [SerializeField]
    private float moveSpeed = 1f;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        GetInput();

        MoveCharacter();

        AnimateCharacter();
    }

    void MoveCharacter()
    {
        rb.MovePosition(transform.position + changeMovementInput.normalized * Time.deltaTime * moveSpeed);
    }

    void GetInput()
    {
        changeMovementInput = Vector3.zero;
        changeMovementInput.x = Input.GetAxisRaw("Horizontal");
        changeMovementInput.y = Input.GetAxisRaw("Vertical");
    }

    void AnimateCharacter()
    {
        if(changeMovementInput != Vector3.zero)
        {
            anim.SetFloat("moveX", changeMovementInput.x);
            anim.SetFloat("moveY", changeMovementInput.y);
            anim.SetBool("moving", true);
        }
        else
        {
            anim.SetBool("moving", false);
        }
    }
}
