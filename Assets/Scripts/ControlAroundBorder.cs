using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlAroundBorder : MonoBehaviour
{
    [SerializeField]
    GameObject insideObject;

    [SerializeField]
    bool pressedState;

    [SerializeField]
    bool isFather;
   
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            pressedState = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            pressedState = false;
        }

        if (!isFather && pressedState)
        {
            if (insideObject != null)
            {
                insideObject.GetComponent<ProjectileLookAt>().StopMovement();
                insideObject.GetComponent<ProjectileLookAt>().StartTimer();
                insideObject.transform.parent = transform;
                isFather = true;
            }
        }

        if(isFather && !pressedState)
        {
            if (insideObject != null)
            {
                insideObject.GetComponent<ProjectileLookAt>().ReverseDirection();
                insideObject.GetComponent<ProjectileLookAt>().StopTimer();
                insideObject.transform.parent = null;
                isFather = false;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        insideObject = collision.gameObject;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        insideObject = null;
    }
}
