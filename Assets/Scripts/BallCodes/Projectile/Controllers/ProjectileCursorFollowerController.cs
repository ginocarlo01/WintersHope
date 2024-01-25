using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileCursorFollowerController : ProjectileController, IPooledObject
{
    public new void OnObjectSpawn()
    {
        InitMaterial();

        Invoke("TurnOff", timeToDestroy);
        currentState = followCursorState;
        currentState.OnBeginState();
    }

    void Update()
    {
        if (currentState != null)
            currentState.OnUpdate();
    }
}
