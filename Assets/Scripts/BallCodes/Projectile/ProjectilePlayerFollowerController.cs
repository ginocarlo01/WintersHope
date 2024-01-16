using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePlayerFollowerController : ProjectileController
{
    void Start()
    {
        InitiMaterial();

        currentState = followAttackState;
        currentState.OnBeginState();
    }

    void Update()
    {

        if (currentState != null)
            currentState.OnUpdate();
    }

}
