using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 2f;

    [SerializeField]
    private float dodgeDistance = 6f;

    [SerializeField]
    private float dodgeCoolDown = 1f;

    private float dodgeTimer = 0f;

    private bool canDodge;

    [SerializeField]
    TrailRenderer trail;

    [SerializeField]
    float trailActiveTime = 0.5f;

    Vector3 mousePos;

    Vector3 worldMousePos;

    Vector3 cursorDirection;

    private void Start()
    {
        trail.emitting = false;
    }


    void Update()
    {
        
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movementDirection = new Vector2(horizontal, vertical);

        movementDirection.Normalize();

        transform.position += movementDirection * moveSpeed * Time.deltaTime;
        

        dodgeTimer += Time.deltaTime;

        if (dodgeTimer > dodgeCoolDown)
        {
            canDodge = true;
            dodgeTimer = 0;
        }

       /* CalculateDirection();

        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(cursorDirection * moveSpeed * Time.deltaTime);
        }
       */

        if (Input.GetKeyDown(KeyCode.Space) && canDodge)
        {
            canDodge = false;
            transform.Translate(movementDirection * dodgeDistance);
            trail.emitting = true;
            StartCoroutine(DisableTrail());
        }
    }


    public void UpgradeMoveSpeed(float upValue)
    {
        moveSpeed += upValue;
    }

    public void ReduceMoveSpeed(float downValue)
    {
        moveSpeed -= downValue;
    }

    public void CalculateDirection()
    {
        mousePos = Input.mousePosition;

        worldMousePos = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10f)); // Set the Z coordinate as per your scene

        cursorDirection = (worldMousePos - transform.position).normalized;
    }

    public void PlayerAttack()
    {

    }

    private IEnumerator DisableTrail()
    {
        yield return new WaitForSeconds(trailActiveTime);
        trail.emitting = false;
    }
}
