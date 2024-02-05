using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTransition : MonoBehaviour
{
    [SerializeField]
    Vector2 cameraChange;

    [SerializeField]
    Vector3 playerChange;

    private CameraFollow camFollow;

    public static Action<Vector2> PlayerEnteredTransition;

   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            //camFollow.AddMaxPosition(cameraChange);
            //camFollow.AddMinPosition(cameraChange);
            PlayerEnteredTransition?.Invoke(cameraChange);
            collision.transform.position += playerChange;
        }
    }
}
