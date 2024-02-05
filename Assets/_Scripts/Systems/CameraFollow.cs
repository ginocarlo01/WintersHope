using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Header("Position Variables")]
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Vector3 offset;
    [SerializeField]
    private float smoothing = 5.0f;


    private void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    private void LateUpdate()
    {
        Vector3 targetPosition = target.position + offset;

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }

}
