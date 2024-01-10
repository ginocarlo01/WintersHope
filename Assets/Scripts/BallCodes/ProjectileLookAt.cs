using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ProjectileLookAt : MonoBehaviour
{
    [SerializeField] private float speed = 3f;

    [SerializeField]
    private float damage = 2f;

    [SerializeField]
    private float destroyTime = 7f;

    

    [SerializeField]
    Vector3 lookAtPos;

    [SerializeField]
    float upSpeedValue = .5f;

    [SerializeField]
    float maxTimer = 7f;


    float timerCountDown;

    bool inTimerState;

    Vector3 movementDirection = new Vector3(1, 0, 0);

    void Start()
    {
        timerCountDown = maxTimer;
    
        Destroy(this.gameObject, destroyTime);

        Vector3 lookDir = lookAtPos - transform.position;

        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    void Update()
    {
        transform.Translate(speed * movementDirection * Time.deltaTime);

        if (inTimerState)
        {
            timerCountDown -= Time.deltaTime;

            if(timerCountDown < 0)
            {
                StopTimer();

                ReverseDirection();

                inTimerState = false;
            }
        }

    }

    public void ReverseDirection()
    {
        movementDirection = new Vector3(-1, 0, 0);
    }

    public void StartTimer()
    {
        inTimerState = true;
    }

    public void StopTimer()
    {
        inTimerState = false;
    }

    public void StopMovement()
    {
        movementDirection = Vector3.zero;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        
    }
}
