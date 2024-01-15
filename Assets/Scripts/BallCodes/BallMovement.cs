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

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        GetInput();

        MoveCharacter();
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
}
