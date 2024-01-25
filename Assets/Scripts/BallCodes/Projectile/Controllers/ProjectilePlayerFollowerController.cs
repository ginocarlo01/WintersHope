using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayerFollowerController : ProjectileController, IPooledObject

{
    public new void OnObjectSpawn()
    {
        InitMaterial();
        
        Invoke("TurnOff", timeToDestroy);
        currentState = followAttackState;
        currentState.OnBeginState();
    }

    void Update()
    {
        if (currentState != null)
            currentState.OnUpdate();
    }



}
