using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField]
    Room[] rooms;

    private Vector3 lastPosition;

    private void OnEnable()
    {
        Room.SavePlayerPosAction += SavePlayerPosition;
    }

    private void OnDisable()
    {
        Room.SavePlayerPosAction -= SavePlayerPosition;
    }

    private void SavePlayerPosition(Vector3 position_)
    {
        lastPosition = position_;
    }

    public Vector3 GetLastPosition()
    {
        return lastPosition;
    }
}
