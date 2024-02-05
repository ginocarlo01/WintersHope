using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementByRotation : MonoBehaviour
{

    [SerializeField]
    float rotationSpeed = 5f;

    void Update()
    {
        transform.Rotate(transform.forward, rotationSpeed * Time.deltaTime);
    }
}
